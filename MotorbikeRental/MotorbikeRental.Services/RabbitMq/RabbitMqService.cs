using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MotorbikeRental.Services.RabbitMq
{
    public class RabbitMqService<T> : IRabbitMqService<T>
    {
        private ConnectionFactory connectionFactory;

        public RabbitMqService(IConfiguration configuration)
        {
            connectionFactory = new ConnectionFactory()
            {
                HostName = RabbitHostName(configuration),
                UserName = RabbitUserName(configuration),
                Password = RabbitPassword(configuration)
            };
        }

        public void SendMessage(T message, string queueName)
        {
            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
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