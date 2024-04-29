using MotorbikeRental.Infrastructure.Models;

namespace MotorbikeRental.Infrastructure.Repositories.IRepositories
{
    public interface IMotorbikeRentalRepository
    {
        Task InsertMotorbikeRentalAsync(MotorbikeRentals motorbikeRentals);
        Task<bool> IsRentedMotorbikeAsync(string plate);
        Task<MotorbikeRentals> GetMotorbikeRentalsAsync(string motorbikePlate, string courierCnpj, string courierRegisterNumber);
        bool IsRentedMotorbikes(string plate);
        void InsertMotorbikeRentals(MotorbikeRentals motorbikeRentals);
    }
}