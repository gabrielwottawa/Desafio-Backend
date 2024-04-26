namespace MotorbikeRental.Infrastructure.Models
{
    public class Couriers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string RegisterNumber { get; set; }
        public int RegisterTypeId { get; set; }
        public string UrlImage { get; set; }
    }
}