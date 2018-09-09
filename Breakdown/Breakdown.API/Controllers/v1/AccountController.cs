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
using Breakdown.API.ViewModels.Account;
using Microsoft.EntityFrameworkCore;
using Breakdown.API.Constants;

namespace Breakdown.API.Controllers.v1
{
    [ApiController]
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

        [HttpPost("api/v1/Account/Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (model == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.RequestContentNull });
            }

            try
            {
                if (!TryValidateModel(model))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        IsSucceeded = false,
                        Response = ResponseConstants.ValidationFailure
                    });
                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        IsSucceeded = false,
                        Response = ResponseConstants.InvalidCredentials
                    });
                }

                var appUser = await _userManager.Users.Include(u => u.Service).SingleAsync(u => u.Email == model.Email);
                var roles = await _userManager.GetRolesAsync(appUser);

                model.UserId = appUser.Id;
                model.ServiceId = appUser.ServiceId;
                model.Name = appUser.Name;
                model.Email = appUser.Email;
                model.Country = appUser.Country;
                model.PhoneNumber = appUser.PhoneNumber;
                model.Token = TokenFactory.GenerateJwtToken(model.Email, appUser, _configuration);
                model.RoleName = roles.FirstOrDefault();
                model.Password = string.Empty;
                if (appUser.Service != null)
                {
                    model.ServiceName = appUser.Service.ServiceName;
                    model.UniqueCode = appUser.Service.UniqueCode;
                }

                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = result.Succeeded, AuthData = model });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    IsSucceeded = false,
                    Response = ResponseConstants.InternalServerError
                });
            }
        }

        [HttpPost("api/v1/Account/Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (model == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.RequestContentNull });
            }

            try
            {
                if (!TryValidateModel(model))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        IsSucceeded = false,
                        Response = ResponseConstants.ValidationFailure
                    });
                }

                if (!await _roleManager.RoleExistsAsync(model.RoleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole<int>() { Name = model.RoleName });
                }

                var user = new ApplicationUser
                {
                    Name = model.Name,
                    Country = model.Country,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email,
                    Email = model.Email,
                    ServiceId = model.ServiceId
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status417ExpectationFailed, new
                    {
                        IsSucceeded = false,
                        Response = ResponseConstants.CreateFailed
                    });
                }

                if (!await _userManager.IsInRoleAsync(user, model.RoleName))
                {
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                }

                await _signInManager.SignInAsync(user, false);
                return StatusCode(StatusCodes.Status201Created, new { IsSucceeded = result.Succeeded });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    IsSucceeded = false,
                    Response = ResponseConstants.InternalServerError
                });
            }
        }
    }
}