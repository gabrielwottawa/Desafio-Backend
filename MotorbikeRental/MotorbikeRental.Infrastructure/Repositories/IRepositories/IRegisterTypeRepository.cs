using MotorbikeRental.Infrastructure.Models;

namespace MotorbikeRental.Infrastructure.Repositories.IRepositories
{
    public interface IRegisterTypeRepository
    {
        Task<RegisterType> GetRegisterTypeByTypeAsync(string registerType);
        Task<RegisterType> GetRegisterTypeByIdAsync(int id);
    }
}
