using Breakdown.Domain.Entities;
using Breakdown.EndSystems.IdentityConfig;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Breakdown.API.Utilities
{
    public static class JwtExtensions
    {
        public static Task ValidateToken(TokenValidatedContext context)
        {
            try
            {
                var serviceProvider = context.HttpContext.RequestServices;
                {
                    var dbService = serviceProvider.GetRequiredService<ApplicationDbContext>();
                    if (dbService == null)
                    {
                        context.Fail("Unable to process");
                        return Task.CompletedTask;
                    }
                    else
                    {
                        var authorization = context.HttpContext.Request.Headers["Authorization"];
                        if (authorization.Count > 0 && !string.IsNullOrWhiteSpace(authorization[0]))
                        {
                            var authToken = authorization[0].Replace("Bearer ", string.Empty).Trim();

                            var invalidatedToken = dbService.ExpiredTokens.SingleOrDefault(et => et.Token == authToken);
                            if (invalidatedToken != null)
                            {
                                context.Fail("User has logged out");
                                return Task.CompletedTask;
                            }
                            return Task.CompletedTask;
                        }
                        else
                        {
                            context.Fail("Unable to process");
                            return Task.CompletedTask;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                context.Fail("Unable to process");
                return Task.CompletedTask;
            }
        }
    }
}
