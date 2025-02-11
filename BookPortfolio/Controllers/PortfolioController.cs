using BookPortfolio.Data;
using BookPortfolio.Dtos.Portfolios;
using BookPortfolio.Extensions;
using BookPortfolio.Interfaces;
using BookPortfolio.Mappers;
using BookPortfolio.Models;
using BookPortfolio.Repositorys;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookPortfolio.Controllers
{
     [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class PortfolioController : Controller
    {

        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IBookRepository _bookRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOLService _oLService;

        public async Task<IActionResult> Index()
        {


            var username = User.Identity.Name;
            if (username == null)
            {
                Console.WriteLine(" User is NOT authenticated (from user.identity)!");
                return RedirectToAction("Login", "Account");
            }
            var user = await _userManager.GetUserAsync(User);
              if (user == null)
            {
                Console.WriteLine(" User is NOT authenticated (from UserManager)!");
                return RedirectToAction("Login", "Account");
            }

            Console.WriteLine($" User is authenticated: {user.UserName}");

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(user);
            return View(userPortfolio);
        }


        public PortfolioController(IPortfolioRepository portfolioRepository, IBookRepository bookRepository ,UserManager<AppUser> userManager, IOLService oLService)
        {
            _portfolioRepository = portfolioRepository;
            _bookRepository = bookRepository;
            _userManager = userManager; 
            _oLService = oLService; 
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            return View(userPortfolio); 
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(CreatePortfolioDto createPortfolioDto)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var book = await _bookRepository.GetByISBNAsync(createPortfolioDto.ISBN);
            if (book == null)
            {
                book = await _oLService.FindBookByISBNSync(createPortfolioDto.ISBN);
                if (book == null)
                {
                    return View(createPortfolioDto);

                }
                else
                {
                    Console.WriteLine("Book FOund");
                    await _bookRepository.CreateAsync(book);
                }
            }
            if (book == null)
            {
                return View(createPortfolioDto);
            }
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            if (userPortfolio.Any(b => b.ISBN_10 == createPortfolioDto.ISBN || b.ISBN_13 == createPortfolioDto.ISBN)) return BadRequest("Already in portfolio");

            var portfolioModel = createPortfolioDto.ToPortfolioFromCreateDTO(appUser.Id, book.Id);
            await _portfolioRepository.CreateAsync(portfolioModel);
            if (portfolioModel == null) return View(createPortfolioDto);
            else return RedirectToAction("Manage") ;
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string ISBN)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            var filteredBook = userPortfolio.Where(b => b.ISBN_10?.ToLower() == ISBN || b.ISBN_13?.ToLower() == ISBN).ToList();

            if (filteredBook.Count() == 1)
            {
                await _portfolioRepository.DeletePortfolioISBN(appUser, filteredBook[0]);
            }
            else return View(ISBN);
            return View("Manage");


        }
    }
}
