using Microsoft.Extensions.Configuration;
using MotorbikeRental.Infrastructure.PostgreSQL;
using MotorbikeRental.Infrastructure.Repositories;
using RabbitMQ.Client;

namespace MotorbikeRental.Background.Consumer.ConsumerBase
{
    public abstract class ConsumerBackgroundContext
    {
        public IConnection ConnectionRabbitMq { get; }

        protected ConsumerBackgroundContext()
        {
            var config = GetAppSettings();
            var factory = new ConnectionFactory()
            {
                HostName = RabbitHostName(config),
                UserName = RabbitUserName(config),
                Password = RabbitPassword(config)
            };

            ConnectionRabbitMq = factory.CreateConnection();
        }

        public CouriersRepository CouriersRepository()
        {
            var _postgreSQLContext = ConnectionPostgre();

            return new CouriersRepository(_postgreSQLContext);
        }

        public MotorbikeRentalRepository MotorbikeRentalRepositorys()
        {
            var _postgreSQLContext = ConnectionPostgre();

            return new MotorbikeRentalRepository(_postgreSQLContext);
        }

        private static PostgreSQLContext ConnectionPostgre()
        {
            var config = GetAppSettings();
            var connectionString = ConnectionString(config);
            return new PostgreSQLContext(connectionString);
        }

        private static IConfigurationRoot GetAppSettings()
        {
            return new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .Build();
        }

        private static string ConnectionString(IConfigurationRoot configuration)
            => configuration.GetSection("ConnectionStrings:PostgreSQLConnectionString").Value;

        private static string RabbitHostName(IConfiguration configuration)
            => configuration.GetSection("RabbitMQConnection:HostName").Value;

        private static string RabbitUserName(IConfiguration configuration)
            => configuration.GetSection("RabbitMQConnection:UserName").Value;

        private static string RabbitPassword(IConfiguration configuration)
            => configuration.GetSection("RabbitMQConnection:Password").Value;
    }
}