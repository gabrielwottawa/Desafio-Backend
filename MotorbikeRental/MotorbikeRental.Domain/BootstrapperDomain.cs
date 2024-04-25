using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MotorbikeRental.Domain.Commands.Auth;
using MotorbikeRental.Domain.Commands.Test;
using MotorbikeRental.Domain.Handlers.Auth;
using MotorbikeRental.Domain.Handlers.Test;

namespace MotorbikeRental.Domain
{
    public static class BootstrapperDomain
    {
        public static void AddDomainBootstrapper(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            #region TestController
            services.AddScoped<IRequestHandler<TestCommand, string>, TestHandler>();
            #endregion

            #region AuthController
            services.AddScoped<IRequestHandler<AuthCommand, string>, AuthHandler>();
            #endregion
        }
    }
}