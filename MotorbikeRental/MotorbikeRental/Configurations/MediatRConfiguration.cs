using FluentValidation;
using System.Reflection;

namespace MotorbikeRental.Configurations
{
    public static class MediatRConfiguration
    {
        public static WebApplicationBuilder AddMediatRConfiguration(this WebApplicationBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            var assembly = AppDomain.CurrentDomain.Load("MotorbikeRental.Domain"); //Assembly.Load("MotorbikeRental.Domain");
            AssemblyScanner
            .FindValidatorsInAssembly(assembly)
                .ForEach(result => builder.Services.AddScoped(result.InterfaceType, result.ValidatorType));

            return builder;
        }
    }
}