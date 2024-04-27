using Microsoft.Extensions.Configuration;
using MotorbikeRental.Infrastructure.Models;
using MotorbikeRental.Infrastructure.PostgreSQL;
using MotorbikeRental.Infrastructure.Repositories;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MotorbikeRental.Background.Consumer
{
    public class RabbitConsumerBackground
    {
        private readonly PostgreSQLContext _postgreSQLContext;
        private readonly CouriersRepository _couriersRepository;
        private readonly IConfiguration configuration;

        public RabbitConsumerBackground(IConfiguration configuration)
        {
            _postgreSQLContext = new PostgreSQLContext(configuration);
            _couriersRepository = new CouriersRepository(_postgreSQLContext);
        }

        public void ConsumeCreateCouriers()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "123"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "QueueCouriersCreate",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);

                    var mensagem = JsonSerializer.Deserialize<Couriers>(json);

                    if (mensagem != null)
                        _couriersRepository.InsertCourier(mensagem);

                    System.Threading.Thread.Sleep(1000);

                    Console.WriteLine($"{json}");
                };
                channel.BasicConsume(queue: "QueueCouriersCreate",
                                     autoAck: true,
                                     consumer: consumer);
            }            
        }
    }
}