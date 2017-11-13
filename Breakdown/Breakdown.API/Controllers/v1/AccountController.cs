﻿using System;
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
        /// <param name="loginmodel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginmodel)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(loginmodel.Email, loginmodel.Password, false, false);

                if (result.Succeeded)
                {
                    var appUser = await _userManager.FindByEmailAsync(loginmodel.Email);
                    var roles = await _userManager.GetRolesAsync(appUser);

                    loginmodel.FirstName = appUser.FirstName;
                    loginmodel.LastName = appUser.LastName;
                    loginmodel.Email = appUser.Email;
                    loginmodel.Country = appUser.Country;
                    loginmodel.PhoneNumber = appUser.PhoneNumber;
                    loginmodel.Token = await TokenFactory.GenerateJwtToken(loginmodel.Email, appUser, _configuration);
                    loginmodel.RoleName = roles.FirstOrDefault();

                    return StatusCode(StatusCodes.Status200OK, loginmodel);
                }
                else
                {
                    return StatusCode(StatusCodes.Status417ExpectationFailed, result);
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
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }

                if (!await _roleManager.RoleExistsAsync(registerModel.RoleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole() { Name = registerModel.RoleName });
                }

                var user = new ApplicationUser
                {
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    Country = registerModel.Country,
                    PhoneNumber = registerModel.PhoneNumber,
                    UserName = registerModel.Email,
                    Email = registerModel.Email
                };

                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (!await _userManager.IsInRoleAsync(user, registerModel.RoleName))
                {
                    await _userManager.AddToRoleAsync(user, registerModel.RoleName);
                }

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    registerModel.Token = await TokenFactory.GenerateJwtToken(registerModel.Email, user, _configuration);
                    return Created("", registerModel); 
                }
                else
                {
                    return StatusCode(StatusCodes.Status417ExpectationFailed, result.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}