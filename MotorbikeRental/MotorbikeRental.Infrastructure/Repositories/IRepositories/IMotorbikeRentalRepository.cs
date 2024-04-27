using MotorbikeRental.Infrastructure.Models;

namespace MotorbikeRental.Infrastructure.Repositories.IRepositories
{
    public interface IMotorbikeRentalRepository
    {
        Task InsertMotorbikeRental(MotorbikeRentals motorbikeRentals);
        Task<bool> MotorbikeIsRented(string plate);
    }
}