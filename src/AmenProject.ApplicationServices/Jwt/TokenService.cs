using DEMAT.ApplicationServices.Helper;
using DEMAT.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.Jwt
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        static Random random = new Random();


        public TokenService(IConfiguration config,
            UserManager<AppUser> userManager
            , RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentVariablesHelper.GetTokenKey()));
        } 


        public async Task<string> CreateToken(AppUser user)
        {
            var claims = await GetAllValidClaims(user);
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = EnvironmentVariablesHelper.GetTokenIssuer()
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GetRandomHexNumber(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }

        public string ToSHA256(string textToHash)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(textToHash));

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length;i++)
            {
                stringBuilder.Append(bytes[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public string DateTimeToTimestamp(DateTime date)
        {
            var UnixTimeStamp = date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var timeStamp = Convert.ToInt64(UnixTimeStamp).ToString();
            return timeStamp;
        }
        
        //Get all valid Claims
        private async Task<List<Claim>> GetAllValidClaims(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email , user.Email),
                new Claim(JwtRegisteredClaimNames.Sub , user.DisplayName)

            };
            //Get user role
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }
            return claims;
        }


    }
}
