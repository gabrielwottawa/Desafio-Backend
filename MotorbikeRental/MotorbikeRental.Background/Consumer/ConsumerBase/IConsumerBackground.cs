namespace MotorbikeRental.Background.Consumer.ConsumerBase
{
    public interface IConsumerBackground
    {
        bool ExistsMessages(string queue);
        void ConsumeCreateCouriers(string queue);
    }
}
