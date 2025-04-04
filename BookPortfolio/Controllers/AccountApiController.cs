﻿using BookPortfolio.Dtos.Account;
using BookPortfolio.Interfaces;
using BookPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookPortfolio.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountApiController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
            if (user == null) { return Unauthorized("Invalid Username"); }
            //var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, true, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Username not found and/or password is not correct");
            }
            return Ok(new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
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
                        return Ok(
                            new NewUserDto
                            {
                                UserName = AppUser.UserName,
                                Email = AppUser.Email,
                                Token = _tokenService.CreateToken(AppUser)
                            });

                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);

                    }
                }
                else
                {
                    return StatusCode(500, createNewUser.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
