using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MotorbikeRental.Background.Consumer;

namespace MotorbikeRental.Background
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var serviceProvider = host.Services;

            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var consumer = new RabbitConsumerBackground(configuration);

            // Inicia o consumo de mensagens a cada 10 segundos
            var timer = new Timer(_ =>
            {
                consumer.ConsumeCreateCouriers();
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile($"appsettings.Development.json");
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<RabbitConsumerBackground>();
                });
    }
}