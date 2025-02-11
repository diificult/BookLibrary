using BookPortfolio.Dtos.Account;
using BookPortfolio.Interfaces;
using BookPortfolio.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
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

            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(loginDto);
            }

            var passwordValid = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!passwordValid.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(loginDto);
            }

            await _signInManager.SignInAsync(user, isPersistent: true, authenticationMethod: CookieAuthenticationDefaults.AuthenticationScheme);

            Console.WriteLine($"User {user.UserName} logged in successfully!");

            return RedirectToAction("Index", "Portfolio");

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
                if (roleResult.Succeeded)
                {
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
