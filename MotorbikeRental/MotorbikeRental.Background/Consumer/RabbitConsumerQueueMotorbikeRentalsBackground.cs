using MotorbikeRental.Background.Consumer.ConsumerBase;
using MotorbikeRental.Domain.Enum;
using MotorbikeRental.Infrastructure.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MotorbikeRental.Background.Consumer
{
    public class RabbitConsumerQueueMotorbikeRentalsBackground
    {
        private ConsumerBackgroundContext _consumerBackground;

        public RabbitConsumerQueueMotorbikeRentalsBackground()
        {
            _consumerBackground = new ConsumerBackground();
        }

        public void ConsumeCreate(string queue)
        {
            var old = new MotorbikeRentals();
            using IModel channel = CreateChannel(queue);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var message = JsonSerializer.Deserialize<MotorbikeRentals>(json);

                var rental = _consumerBackground.MotorbikeRentalRepositorys().IsRentedMotorbikes(message.MotorbikePlate);

                if (rental)
                {
                    Console.WriteLine($"Rejected: {json}");
                    message.Status = (int)MotorbikeRentalStatus.Rejected;
                    _consumerBackground.MotorbikeRentalRepositorys().InsertMotorbikeRentals(message);
                }
                else
                {
                    Console.WriteLine($"Approved: {json}");
                    message.ActiveRental = 1;
                    message.Status = (int)MotorbikeRentalStatus.Approved;
                    old = message;
                    _consumerBackground.MotorbikeRentalRepositorys().InsertMotorbikeRentals(message);
                }
            };

            channel.BasicConsume(queue: queue,
                                 autoAck: true,
                                 consumer: consumer);
        }

        public bool ExistsMessages(string queue)
        {
            using IModel channel = CreateChannel(queue);
            var messageCount = channel.MessageCount(queue);
            return messageCount > 0;
        }

        private IModel CreateChannel(string queue)
        {
            var channel = _consumerBackground.ConnectionRabbitMq.CreateModel();
            channel.QueueDeclare(queue: queue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            return channel;
        }
    }
}