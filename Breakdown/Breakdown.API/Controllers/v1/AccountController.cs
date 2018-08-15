using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Breakdown.Contracts.DTOs;
using Breakdown.API.TokenUtilities;
using Breakdown.Domain.Entities;
using System.Net;
using System.Net.Http;
using Breakdown.API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Breakdown.API.Controllers.v1
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole<int>> roleManager,
            IConfiguration configuration
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, false, false);

                if (result.Succeeded)
                {
                    //var appUser = await _userManager.FindByEmailAsync(loginViewModel.Email);
                    var appUser = await _userManager.Users.Include(u => u.Service).SingleAsync(u => u.Email == loginViewModel.Email);
                    var roles = await _userManager.GetRolesAsync(appUser);

                    loginViewModel.UserId = appUser.Id;
                    loginViewModel.ServiceId = appUser.ServiceId;
                    loginViewModel.Name = appUser.Name;
                    loginViewModel.Email = appUser.Email;
                    loginViewModel.Country = appUser.Country;
                    loginViewModel.PhoneNumber = appUser.PhoneNumber;
                    loginViewModel.Token = TokenFactory.GenerateJwtToken(loginViewModel.Email, appUser, _configuration);
                    loginViewModel.RoleName = roles.FirstOrDefault();
                    loginViewModel.Password = string.Empty;
                    if (appUser.Service != null)
                    {
                        loginViewModel.ServiceName = appUser.Service.ServiceName;
                        loginViewModel.UniqueCode = appUser.Service.UniqueCode;
                    }

                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = result.Succeeded, AuthData = loginViewModel });
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = result.Succeeded, Error = "Invalid email or password." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSucceeded = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = ModelState.IsValid, Errors = ModelState });
                }

                if (!await _roleManager.RoleExistsAsync(registerViewModel.RoleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole<int>() { Name = registerViewModel.RoleName });
                }

                var user = new ApplicationUser
                {
                    Name = registerViewModel.Name,
                    Country = registerViewModel.Country,
                    PhoneNumber = registerViewModel.PhoneNumber,
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                    ServiceId = registerViewModel.ServiceId
                };

                var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    if (!await _userManager.IsInRoleAsync(user, registerViewModel.RoleName))
                    {
                        await _userManager.AddToRoleAsync(user, registerViewModel.RoleName);
                    }

                    await _signInManager.SignInAsync(user, false);

                    var appUser = await _userManager.Users.Include(u => u.Service).SingleAsync(u => u.Email == registerViewModel.Email);

                    registerViewModel.UserId = user.Id;
                    registerViewModel.ServiceId = user.ServiceId;
                    registerViewModel.Token = TokenFactory.GenerateJwtToken(registerViewModel.Email, user, _configuration);
                    registerViewModel.Password = string.Empty;
                    if (appUser.Service != null)
                    {
                        registerViewModel.ServiceName = appUser.Service.ServiceName;
                        registerViewModel.UniqueCode = appUser.Service.UniqueCode;
                    }

                    return Created("", new { IsSucceeded = result.Succeeded, AuthData = registerViewModel });
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = result.Succeeded, Error = result.Errors });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { IsSucceeded = false });
            }
        }
    }
}