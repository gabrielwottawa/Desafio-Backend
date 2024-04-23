using MotorbikeRental.Infrastructure.Models;

namespace MotorbikeRental.Infrastructure.Repositories.IRepositories
{
    public interface IMotorbikeRepository
    {
        Task<IEnumerable<Motorbike>> GetAllMotorbike();
    }
}
