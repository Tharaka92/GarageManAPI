
using Breakdown.Contracts.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Breakdown.API.Utilities
{
    public static class JwtFactory
    {
        public static async Task<string> GenerateJwtToken(string email,
                                                          IdentityUser<int> user,
                                                          IConfiguration configuration,
                                                          JwtOptions jwtOptions,
                                                          string roleName)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, await jwtOptions.JtiGenerator()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, roleName)
            };

             var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                notBefore: jwtOptions.NotBefore,
                expires: jwtOptions.Expiration,
                signingCredentials: jwtOptions.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
