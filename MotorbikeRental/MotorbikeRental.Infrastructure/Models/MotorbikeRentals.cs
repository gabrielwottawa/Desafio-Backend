namespace MotorbikeRental.Infrastructure.Models
{
    public class MotorbikeRentals
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public int RentalPlansId { get; set; }
        public int MotorbikeId { get; set; }
        public string MotorbikePlate { get; set; }
        public int CourierId { get; set; }
        public string CourierCnpj { get; set; }
        public string CourierRegisterNumber { get; set; }
        public int ActiveRental { get; set; }
        public int Status { get; set; }
    }
}