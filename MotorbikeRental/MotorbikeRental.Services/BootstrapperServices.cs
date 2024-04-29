using Microsoft.Extensions.DependencyInjection;
using MotorbikeRental.Services.Auth;

namespace MotorbikeRental.Services
{
    public static class BootstrapperServices
    {
        public static void AddServicesBootstrapper(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IAuthService, AuthService>();
        }
    }
}