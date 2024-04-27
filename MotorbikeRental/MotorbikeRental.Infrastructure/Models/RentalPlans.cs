namespace MotorbikeRental.Infrastructure.Models
{
    public class RentalPlans
    {
        public int Id { get; set; }
        public int NumberDays { get; set; }
        public decimal ValuePerDay { get; set; }
    }
}