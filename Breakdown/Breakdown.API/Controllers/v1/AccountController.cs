using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Breakdown.Contracts.Options;
using Breakdown.API.Utilities;
using Breakdown.Domain.Entities;
using System.Net;
using System.Net.Http;
using Breakdown.API.ViewModels.Account;
using Microsoft.EntityFrameworkCore;
using Breakdown.API.Constants;
using Microsoft.Extensions.Options;
using Breakdown.Contracts.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Breakdown.API.Controllers.v1
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly JwtOptions _jwtOptions;
        private readonly IExpiredTokenRepository _expiredTokenRepository;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole<int>> roleManager,
            IConfiguration configuration,
            IOptions<JwtOptions> jwtOptions,
            IExpiredTokenRepository expiredTokenRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _jwtOptions = jwtOptions.Value;
            _expiredTokenRepository = expiredTokenRepository;
        }

        [HttpPost("api/v1/Account/Login")]
        public async Task<IActionResult> Login(LoginRequestViewModel model)
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

                var appUser = await _userManager.Users.Include(u => u.Service).SingleOrDefaultAsync(u => u.Email == model.Email);
                var roles = await _userManager.GetRolesAsync(appUser);

                LoginResponseViewModel loginResponse = new LoginResponseViewModel
                {
                    UserId = appUser.Id,
                    ServiceId = appUser.ServiceId,
                    Name = appUser.Name,
                    Email = appUser.Email,
                    Country = appUser.Country,
                    PhoneNumber = appUser.PhoneNumber,
                    Token = await JwtFactory.GenerateJwtToken(model.Email, appUser, _configuration, _jwtOptions),
                    RoleName = roles.FirstOrDefault(),
                    ProfileImageUrl = appUser.ProfileImageUrl,
                    VehicleNumber = appUser.VehicleNumber,
                    AverageRating = appUser.AverageRating,
                    IsApproved = appUser.IsApproved,
                    IsBlocked = appUser.IsBlocked,
                    HasAPaymentMethod = appUser.HasAPaymentMethod,
                    HasADuePayment = appUser.HasADuePayment
                };

                if (appUser.Service != null)
                {
                    loginResponse.ServiceName = appUser.Service.ServiceName;
                    loginResponse.UniqueCode = appUser.Service.UniqueCode;
                }

                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = result.Succeeded, AuthData = loginResponse });
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
                    VehicleNumber = model.VehicleNumber,
                    UserName = model.Email,
                    Email = model.Email,
                    ServiceId = model.ServiceId,
                    ProfileImageUrl = Request.Scheme + "://" + Request.Host + Request.PathBase + "/images/defaultprofile.jpg",
                    IsApproved = model.IsApproved
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

        [Authorize]
        [HttpGet("api/v1/Account/GetProfileData/{userId:int}")]
        public async Task<IActionResult> GetProfileData(int userId)
        {
            if (userId <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { IsSucceeded = false, Response = ResponseConstants.InvalidData });
            }

            try
            {
                var appUser = await _userManager.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
                if (appUser == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { IsSucceeded = false, Response = ResponseConstants.NotFound });
                }

                UserProfileResponseViewModel userProfileResponseVm = new UserProfileResponseViewModel
                {
                    Name = appUser.Name,
                    Email = appUser.Email,
                    PhoneNumber = appUser.PhoneNumber,
                    VehicleNumber = appUser.VehicleNumber,
                    ProfileImageUrl = appUser.ProfileImageUrl,
                    AverageRating = appUser.AverageRating,
                    IsApproved = appUser.IsApproved,
                    IsBlocked = appUser.IsBlocked,
                    HasAPaymentMethod = appUser.HasAPaymentMethod,
                    HasADuePayment = appUser.HasADuePayment
                };

                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true, Response = userProfileResponseVm });
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

        [Authorize]
        [HttpGet("api/v1/Account/Logout")]
        public async Task<ActionResult> Logout()
        {
            try
            {
                var authorization = HttpContext.Request.Headers["Authorization"];
                if (authorization.Count > 0 && !string.IsNullOrWhiteSpace(authorization[0]))
                {
                    var authToken = authorization[0].Replace("Bearer ", string.Empty).Trim();
                    if (!string.IsNullOrWhiteSpace(authToken))
                    {
                        var handler = new JwtSecurityTokenHandler();
                        var jwtToken = handler.ReadToken(authToken) as JwtSecurityToken;
                        int.TryParse(jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value, out int userId);

                        await _expiredTokenRepository.Create(new ExpiredToken
                        {
                            Token = authToken,
                            ExpiredDate = DateTime.Now,
                            UserId = userId
                        });
                    }
                }

                return StatusCode(StatusCodes.Status200OK, new { IsSucceeded = true });
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