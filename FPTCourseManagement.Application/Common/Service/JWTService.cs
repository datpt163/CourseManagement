using FPTCourseManagement.Domain.Entities.Users;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Application.Common.Service
{
    public interface IJwtService
    {
        Task<string> generatejwttokentw(User account);
    }
    public class JwtService : IJwtService
    {
        public async Task<string> generatejwttokentw(User account)
        {
            var secretkey = "ijurkbdlhmklqacwqzdxmkkhvqowlyqa99";
            var issuer = "localhost:7144";
            List<Claim> claims = new()
            {
        new Claim(ClaimTypes.NameIdentifier, account.Id + ""),
        new Claim(ClaimTypes.Role , account.Role.Name + "")
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtsecuritytoken = new JwtSecurityToken(
                issuer: issuer,
                audience: issuer,
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtsecuritytoken);
        }
    }
}
