using MotorbikeRental.Infrastructure.Models;

namespace MotorbikeRental.Infrastructure.Repositories.IRepositories
{
    public interface IUsersRepository
    {
        Task<Users> GetUser(string name, string password);
        Task UpdateToken(int id, string token, DateTime tokenDateExpire);
    }
}
