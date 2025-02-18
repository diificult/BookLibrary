using BookPortfolio.Dtos.Portfolios;
using BookPortfolio.Interfaces;
using BookPortfolio.Mappers;
using BookPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookPortfolio.Controllers
{
    // [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    
    public class PortfolioController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IOLService _olService;

        public PortfolioController(UserManager<AppUser> userManager, IPortfolioRepository portfolioRepository, IBookRepository bookRepository, IOLService oLService)
        {
            _userManager = userManager;
            _portfolioRepository = portfolioRepository;
            _bookRepository = bookRepository;
            _olService = oLService;
        }

        public async Task<IActionResult> Index()
        {
            Console.WriteLine($" IsAuthenticated? {User.Identity?.IsAuthenticated}");
            Console.WriteLine($" Authentication Type: {User.Identity?.AuthenticationType}");
            Console.WriteLine($" Username: {User.Identity?.Name}");

            var claims = User.Claims.ToList();
            Console.WriteLine(" User Claims:");
            foreach (var claim in claims)
            {
                Console.WriteLine($" - {claim.Type}: {claim.Value}");
            }

            if (!User.Identity.IsAuthenticated)
            {
                Console.WriteLine(" User is NOT authenticated inside PortfolioController!");
                return RedirectToAction("Login", "Account");
            }

            Console.WriteLine($" User {User.Identity.Name} authenticated, fetching portfolio...");

            ViewData["Username"] = User.Identity.Name;

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userPortfolio = await _portfolioRepository.GetUserPortfolioList(user);

            return View(userPortfolio);
        }
        [HttpGet]
        public IActionResult AddPortfolio() => View();
        [HttpPost]
        public async Task<IActionResult> AddPortfolio(CreatePortfolioDto createPortfolioDto)
        {
            var claims = User.Claims.ToList();
            if (!User.Identity.IsAuthenticated)
            {
                Console.WriteLine(" User is NOT authenticated inside PortfolioController!");
                return RedirectToAction("Login", "Account");
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var book = await _bookRepository.GetByISBNAsync(createPortfolioDto.ISBN);
            if (book == null)
            {
                book = await _olService.FindBookByISBNSync(createPortfolioDto.ISBN);
                if (book == null)
                {
                    return View(createPortfolioDto);
                }
                else
                {
                    Console.WriteLine(createPortfolioDto);
                    await _bookRepository.CreateAsync(book);

                }
            }
            if (book == null)
            {
                return View("Book not found");
            }

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(user);
            if (userPortfolio.Any(b => b.ISBN_10 == createPortfolioDto.ISBN || b.ISBN_13 == createPortfolioDto.ISBN)) return BadRequest("Already in portfolio");
            var portfolioModel = createPortfolioDto.ToPortfolioFromCreateDTO(user.Id, book.Id);
            await _portfolioRepository.CreateAsync(portfolioModel);
            if (portfolioModel == null) return StatusCode(500, "Couldn't create");
            else return RedirectToAction("Index", "Portfolio");
        }

        [HttpGet]
        public async Task<IActionResult> UserPortfolio(string username)
        {
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser != null)
            {
                var userPortfolio = await _portfolioRepository.GetUserPortfolioList(appUser);   
                return View("UserPortfolio", userPortfolio);

            }
            else
            {
                return StatusCode(500, "User not found");
            }
        }

    }
}
