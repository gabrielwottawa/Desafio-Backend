using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MotorbikeRental.Services.RabbitMq
{
    public class RabbitMqService<T> : IRabbitMqService<T>
    {
        private ConnectionFactory connectionFactory;

        public RabbitMqService()
        {
            connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "123"
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

        public async Task<T> ConsumeMessageAsync(string queueName)
        {
            T message = default;
            var tcs = new TaskCompletionSource<T>();
            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new AsyncEventingBasicConsumer(channel);

            channel.BasicConsume(queue: queueName,
                                autoAck: true,
                                consumer: consumer);

            consumer.Received += async (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                message = JsonSerializer.Deserialize<T>(json);
                await Task.Yield();
                channel.BasicAck(eventArgs.DeliveryTag, false);
                tcs.SetResult(message);
            };

            //channel.BasicConsume(queue: queueName,
            //                    autoAck: false,
            //                    consumer: consumer);

            return await tcs.Task;
        }
    }
}