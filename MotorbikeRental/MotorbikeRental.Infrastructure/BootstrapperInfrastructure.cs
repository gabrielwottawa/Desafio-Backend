using Microsoft.Extensions.DependencyInjection;
using MotorbikeRental.Infrastructure.PostgreSQL;
using MotorbikeRental.Infrastructure.Repositories;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;

namespace MotorbikeRental.Infrastructure
{
    public static class BootstrapperInfrastructure
    {
        public static void AddInfrastructureBootstrapper(this IServiceCollection services) 
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<PostgreSQLDatabaseContext, PostgreSQLContext>();

            services.AddScoped<IMotorbikeRepository, MotorbikeRepository>();
        }
    }
}