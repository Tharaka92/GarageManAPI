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

namespace Breakdown.API.Controllers.v1
{
    [Produces("application/json")]
    [Route("api/v1/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Login a user to the platform
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginModel)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false);

                if (result.Succeeded)
                {
                    var appUser = await _userManager.FindByEmailAsync(loginModel.Email);
                    var roles = await _userManager.GetRolesAsync(appUser);

                    loginModel.UserId = appUser.Id;
                    loginModel.Name = appUser.Name;
                    loginModel.Email = appUser.Email;
                    loginModel.Country = appUser.Country;
                    loginModel.PhoneNumber = appUser.PhoneNumber;
                    loginModel.Token = await TokenFactory.GenerateJwtToken(loginModel.Email, appUser, _configuration);
                    loginModel.RoleName = roles.FirstOrDefault();

                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = result.Succeeded, AuthData = loginModel });
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = result.Succeeded, Error = "Invalid email or password." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }            
        }

        /// <summary>
        /// Register a new user to the platform
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = ModelState.IsValid, Errors = ModelState });
                }

                if (!await _roleManager.RoleExistsAsync(registerModel.RoleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole() { Name = registerModel.RoleName });
                }

                var user = new ApplicationUser
                {
                    Name = registerModel.Name,
                    Country = registerModel.Country,
                    PhoneNumber = registerModel.PhoneNumber,
                    UserName = registerModel.Email,
                    Email = registerModel.Email
                };

                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {
                    if (!await _userManager.IsInRoleAsync(user, registerModel.RoleName))
                    {
                        await _userManager.AddToRoleAsync(user, registerModel.RoleName);
                    }

                    await _signInManager.SignInAsync(user, false);

                    registerModel.UserId = user.Id;
                    registerModel.Token = await TokenFactory.GenerateJwtToken(registerModel.Email, user, _configuration);

                    return Created("", new { IsSucceeded = result.Succeeded, AuthData = registerModel }); 
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = result.Succeeded, Error = result.Errors  });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}