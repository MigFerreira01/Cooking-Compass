using CookingCompassAPI.Domain;
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

namespace CookingCompassAPI.Services.Authentication.Token
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {

            var jwSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]

            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: jwSettings["Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwSettings["ExpiryMinutes"])), // Token expiration
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public static string GenerateSecureKey(int size = 32) // size in bytes
        {
            var key = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            return Convert.ToBase64String(key); // Store this in your configuration
        }
    }
}
