using BookPortfolio.Extensions;
using BookPortfolio.Interfaces;
using BookPortfolio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookPortfolio.Controllers
{
    [Route("Portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IBookRepository _bookRepository;
        public PortfolioController(UserManager<AppUser> userManager, IPortfolioRepository portfolioRepository, IBookRepository bookRepository)
        {
            _userManager = userManager;
            _portfolioRepository = portfolioRepository;
            _bookRepository = bookRepository;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string ISBN_10)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var book = await _bookRepository.GetByISBNAsync(ISBN_10);
            if (book == null)
            {
                //Find book using API
            }
            if (book == null)
            {
                return BadRequest("Book not found");
            }
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            if (userPortfolio.Any(b => b.ISBN_10 == ISBN_10)) return BadRequest("Already in portfolio");

            var portfolioModel = new Portfolio
            {
                BookId = book.Id,
                AppUserId = appUser.Id,
            };
            await _portfolioRepository.CreateAsync(portfolioModel);
            if (portfolioModel == null) return StatusCode(500, "Couldn't create");
            else return Created();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string ISBN_10)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username); 
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            var filteredBook = userPortfolio.Where(b => b.ISBN_10.ToLower() == ISBN_10);

            if (filteredBook.Count() == 1)
            {
                await _portfolioRepository.DeletePortfolioISBN(appUser, ISBN_10);
            }
            else return BadRequest("Book not in portfolio");
            return Ok();

            
        }
    }
}
