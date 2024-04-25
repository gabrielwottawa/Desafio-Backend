using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MotorbikeRental.Infrastructure.Models;
using MotorbikeRental.Services.Auth.Extension;

namespace MotorbikeRental.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly string _key;

        public AuthService()
        {
            _key = "5SkKvsaSD1rsF358m2HI9EI1ZlVi1bWauUdyam34Jp4=";
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