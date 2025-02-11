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
    // [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class PortfolioController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioController(UserManager<AppUser> userManager, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _portfolioRepository = portfolioRepository;
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
    }

}
