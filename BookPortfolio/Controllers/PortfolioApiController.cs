using BookPortfolio.Dtos.Portfolios;
using BookPortfolio.Extensions;
using BookPortfolio.Interfaces;
using BookPortfolio.Mappers;
using BookPortfolio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookPortfolio.Controllers
{
    [Route("api/Portfolio")]
    [ApiController]
    public class PortfolioApiController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IOLService _olService;
        public PortfolioApiController(UserManager<AppUser> userManager, IPortfolioRepository portfolioRepository, IBookRepository bookRepository, IOLService oLService)
        {
            _userManager = userManager;
            _portfolioRepository = portfolioRepository;
            _bookRepository = bookRepository;
            _olService = oLService;
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
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio([FromBody] CreatePortfolioDto createPortfolioDto)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var book = await _bookRepository.GetByISBNAsync(createPortfolioDto.ISBN);
            if (book == null)
            {
                book = await _olService.FindBookByISBNSync(createPortfolioDto.ISBN);
                if (book == null)
                {
                    return BadRequest("Book does not exist");

                }
                else
                {
                    Console.WriteLine("Book FOund");
                    await _bookRepository.CreateAsync(book);
                }
            }
            if (book == null)
            {
                return BadRequest("Book not found");
            }
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            if (userPortfolio.Any(b => b.ISBN_10 == createPortfolioDto.ISBN || b.ISBN_13 == createPortfolioDto.ISBN)) return BadRequest("Already in portfolio");

            var portfolioModel = createPortfolioDto.ToPortfolioFromCreateDTO(appUser.Id, book.Id);
            await _portfolioRepository.CreateAsync(portfolioModel);
            if (portfolioModel == null) return StatusCode(500, "Couldn't create");
            else return Created();
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
            else return BadRequest("Book not in portfolio");
            return Ok();

            
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateState(string ISBN, string NewState)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortoflio = await _portfolioRepository.GetUserPortfolio(appUser);
            var filteredBook = userPortoflio.Where(b => b.ISBN_10?.ToLower() == ISBN || b.ISBN_13?.ToLower() == ISBN).ToList();

            if (filteredBook.Count() == 1)
            {
                await _portfolioRepository.UpdateStateAsync(appUser, filteredBook[0].Id, NewState);
            }
            else return BadRequest("Book not in portfolio");
            return Ok();
        }

        //Check security for this in future, previous function requires auth but they could get around it using this.
        /*
        [HttpGet("user-reviews/{userId}")]
        public async Task<IActionResult> GetUserReview(string userId)
        {

            var portfolio = await _portfolioRepository.GetUserReviews(userId);
            return Ok(portfolio);
        }
        */
    }
}
