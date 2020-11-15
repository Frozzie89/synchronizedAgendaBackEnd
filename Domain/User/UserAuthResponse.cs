using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TI_BackEnd.Domain.User
{
    public class UserAuthResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }

        public UserAuthResponse() { }

        public UserAuthResponse(User user)
        {
            Id = user.Id;
            Email = user.Email;
            LastName = user.LastName;
            FirstName = user.FirstName;
            UserName = user.UserName;

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this_is_my_custom_Secret_key_for_authentication"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new Claim("id", (user.Id).ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("UserName", user.UserName),
                new Claim("Email", user.Email)
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:5501",
                audience: "https://localhost:5501",
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: signingCredentials
            );

            Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}