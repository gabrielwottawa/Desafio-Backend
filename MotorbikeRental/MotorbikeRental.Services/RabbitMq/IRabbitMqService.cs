namespace MotorbikeRental.Services.RabbitMq
{
    public interface IRabbitMqService<T>
    {
        void SendMessage(T message, string queueName);
    }
}