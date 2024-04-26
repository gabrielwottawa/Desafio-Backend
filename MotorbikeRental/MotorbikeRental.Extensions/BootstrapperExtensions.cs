using Microsoft.Extensions.DependencyInjection;

namespace MotorbikeRental.Extensions
{
    public static class BootstrapperExtensions
    {
        public static void AddBootstrapperExtensions(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.Add<StringExtension>();
        }
    }
}
