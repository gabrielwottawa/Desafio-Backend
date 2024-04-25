using MotorbikeRental.Infrastructure.Models;

namespace MotorbikeRental.Infrastructure.Repositories.IRepositories
{
    public interface IMotorbikeRepository
    {
        Task<IEnumerable<Motorbikes>> GetAllMotorbike();
        Task<Motorbikes> GetMotorbikeByPlate(string plate);
        Task InsertMotorbike(Motorbikes motorbike);
    }
}
