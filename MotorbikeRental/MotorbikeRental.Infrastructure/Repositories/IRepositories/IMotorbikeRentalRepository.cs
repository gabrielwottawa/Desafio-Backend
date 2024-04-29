using MotorbikeRental.Infrastructure.Models;

namespace MotorbikeRental.Infrastructure.Repositories.IRepositories
{
    public interface IMotorbikeRentalRepository
    {
        Task InsertMotorbikeRental(MotorbikeRentals motorbikeRentals);
        Task<bool> IsRentedMotorbike(string plate);
        bool IsRentedMotorbikes(string plate);
        void InsertMotorbikeRentals(MotorbikeRentals motorbikeRentals);
    }
}