using Microsoft.Extensions.DependencyInjection;
using MotorbikeRental.Infrastructure.Migrations;

Console.WriteLine("**** BaseDadosGeograficos - Execucao de Migrations ****");

if (args.Length != 1)
{
    var oldForegroundColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("ConnectionString PostgreSQL");
    Console.ForegroundColor = oldForegroundColor;
    return;
}

var services = new ServiceCollection();
Startup.ConfigureServices(services, args);
services.BuildServiceProvider().GetService<ConsoleApp>().Run();