using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MotorbikeRental.Infrastructure.Models;
using MotorbikeRental.Services.Auth.Extension;

namespace MotorbikeRental.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly string _key;
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = _configuration.GetSection("KeyAuth").Value;
        }

        public string Authenticate(Users users)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, users.Name),
                    new Claim(ClaimTypes.Role, UserRole.GetUserRole(users.UserTypeId))
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Define o tempo de expiração do token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}