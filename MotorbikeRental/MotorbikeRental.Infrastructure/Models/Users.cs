namespace MotorbikeRental.Infrastructure.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public string Token { get; set; }
        public DateTime TokenDateExpire { get; set; }
    }
}