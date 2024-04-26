using MotorbikeRental.Infrastructure.Models;

namespace MotorbikeRental.Infrastructure.Repositories.IRepositories
{
    public interface IMotorbikeRepository
    {
        Task<IEnumerable<Motorbikes>> GetAllMotorbikes(string? plate);
        Task<Motorbikes> GetMotorbikeByPlate(string plate);
        Task<Motorbikes> GetMotorbikeById(int id);
        Task InsertMotorbike(Motorbikes motorbike);
        Task UpdateMotorbike(int id, string plate);
        Task DeleteMotorbikeById(int id);
    }
}