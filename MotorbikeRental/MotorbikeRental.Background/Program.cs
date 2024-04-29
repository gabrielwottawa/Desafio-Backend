using MotorbikeRental.Background.Consumer;

namespace MotorbikeRental.Background
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var consumerQueueCouriers = new RabbitConsumerQueueCouriersBackground();
            var consumerQueueMotorbikeRentals = new RabbitConsumerQueueMotorbikeRentalsBackground();

            while (true)
            {
                if (consumerQueueCouriers.ExistsMessages("QueueCouriersCreate"))
                    consumerQueueCouriers.ConsumeCreateCouriers("QueueCouriersCreate");

                if (consumerQueueMotorbikeRentals.ExistsMessages("QueueMotorbikeRentals"))
                    consumerQueueMotorbikeRentals.ConsumeCreate("QueueMotorbikeRentals");
            }
        }
    }
}