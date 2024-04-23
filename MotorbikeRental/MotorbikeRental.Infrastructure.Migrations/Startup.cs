using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace MotorbikeRental.Infrastructure.Migrations
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services, string[] args)
        {
            Console.WriteLine("Configurando recursos...");

            services.AddLogging();

            services.AddFluentMigratorCore()
                .ConfigureRunner(cfg => cfg
                    .AddPostgres()
                    .WithGlobalConnectionString(args[0])
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                )
                .AddLogging(cfg => cfg.AddFluentMigratorConsole());

            services.AddTransient<ConsoleApp>();
        }
    }
}