using MotorbikeRental.Infrastructure.Models;

namespace MotorbikeRental.Infrastructure.Repositories.IRepositories
{
    public interface IRegisterTypeRepository
    {
        Task<RegisterType> GetRegisterTypeByType(string registerType);
        Task<RegisterType> GetRegisterTypeById(int id);
    }
}
