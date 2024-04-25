using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MotorbikeRental.Domain.Commands.Auth;
using MotorbikeRental.Domain.Commands.Motorbike;
using MotorbikeRental.Domain.Handlers.Auth;
using MotorbikeRental.Domain.Handlers.Motorbike;
using MotorbikeRental.Domain.Responses;

namespace MotorbikeRental.Domain
{
    public static class BootstrapperDomain
    {
        public static void AddDomainBootstrapper(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            #region AuthController
            services.AddScoped<IRequestHandler<AuthCommand, CommandResult>, AuthHandler>();
            #endregion

            #region MotorbikeController
            services.AddScoped<IRequestHandler<CreateMotorbikeCommand, CommandResult>, CreateMotorbikeHandler>();
            #endregion
        }
    }
}