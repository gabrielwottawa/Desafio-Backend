using MotorbikeRental.Infrastructure.Models;

namespace MotorbikeRental.Infrastructure.Repositories.IRepositories
{
    public interface IMotorbikeRepository
    {
        Task<IEnumerable<Motorbikes>> GetAllMotorbikesAsync(string? plate);
        Task<Motorbikes> GetMotorbikeByPlateAsync(string plate);
        Task<Motorbikes> GetMotorbikeByIdAsync(int id);
        Task InsertMotorbikeAsync(Motorbikes motorbike);
        Task UpdateMotorbikeAsync(int id, string plate);
        Task DeleteMotorbikeByIdAsync(int id);
    }
}