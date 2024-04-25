using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace MotorbikeRental.Configurations
{
    public static class JwtBearerConfiguration
    {
        public static WebApplicationBuilder AddJwtBearerConfiguration(this WebApplicationBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            var key = Encoding.UTF8.GetBytes("5SkKvsaSD1rsF358m2HI9EI1ZlVi1bWauUdyam34Jp4=");
            //var key = Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "admin"));
                options.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "user"));
            });

            return builder;
        }
    }
}