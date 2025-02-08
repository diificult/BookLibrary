using BookPortfolio.Dtos.Account;
using BookPortfolio.Interfaces;
using BookPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookPortfolio.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }
        public IActionResult Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }
            var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Portfolio");
            }
            ModelState.AddModelError("", "Invalid Login Attempt");
            return View(loginDto);
        }
        public IActionResult Register() => View();
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid) { return View(registerDto); }
            var AppUser = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.EmailAddress,
            };
            var createNewUser = await _userManager.CreateAsync(AppUser);
            if (createNewUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(AppUser, "User");
                if (roleResult.Succeeded) { 
                    return RedirectToAction("Login");   
                }
                
            }
            ModelState.AddModelError("", "Registation Failed");
            return View(registerDto);
        }
        public async Task<IActionResult> Logout()
        {
               await _signInManager.SignOutAsync(); 
            return RedirectToAction("Index");
        }
    }
}
