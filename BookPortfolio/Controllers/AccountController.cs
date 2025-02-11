using BookPortfolio.Dtos.Account;
using BookPortfolio.Interfaces;
using BookPortfolio.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookPortfolio.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
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

            // Ensure Authentication is applied
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);


            await _signInManager.SignInAsync(user, authProperties, authenticationMethod: CookieAuthenticationDefaults.AuthenticationScheme);

            Console.WriteLine($"User {user.UserName} logged in successfully with Cookie Authentication!");

            return RedirectToAction("Index", "Portfolio");
        }

        public IActionResult Register() => View();
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            Console.WriteLine($"Attempting to create");
            if (!ModelState.IsValid)
            {
                Console.WriteLine($"Incorrect modelstate ");
                return View(registerDto);
            }
            var AppUser = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.EmailAddress,
            };
            var createNewUser = await _userManager.CreateAsync(AppUser, registerDto.Password);
            if (createNewUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(AppUser, "User");
                if (roleResult.Succeeded)
                {
                    Console.WriteLine($"User {AppUser.UserName} created successfully!");
                    return RedirectToAction("Login");
                }

            }
            Console.WriteLine($"User {AppUser.UserName} created failed!");
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
