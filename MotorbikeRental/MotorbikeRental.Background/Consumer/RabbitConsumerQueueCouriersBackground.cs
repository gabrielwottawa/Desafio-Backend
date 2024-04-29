using MotorbikeRental.Infrastructure.Models;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using MotorbikeRental.Background.Consumer.ConsumerBase;

namespace MotorbikeRental.Background.Consumer
{
    public class RabbitConsumerQueueCouriersBackground : IConsumerBackground
    {
        private ConsumerBackgroundContext _consumerBackground;

        public RabbitConsumerQueueCouriersBackground()
        {
            _consumerBackground = new ConsumerBackground();
        }

        public bool ExistsMessages(string queue)
        {
            using IModel channel = CreateChannel(queue);
            var messageCount = channel.MessageCount(queue);
            return messageCount > 0;
        }

        public void ConsumeCreateCouriers(string queue)
        {
            using IModel channel = CreateChannel(queue);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                var message = JsonSerializer.Deserialize<Couriers>(json);

                try
                {
                    _consumerBackground.CouriersRepository().InsertCourier(message);
                } 
                catch (Exception) { }
            };

            channel.BasicConsume(queue: queue,
                                 autoAck: true,
                                 consumer: consumer);
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