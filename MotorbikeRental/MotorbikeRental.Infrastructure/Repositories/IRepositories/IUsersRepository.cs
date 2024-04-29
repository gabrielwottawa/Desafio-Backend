using MotorbikeRental.Infrastructure.Models;

namespace MotorbikeRental.Infrastructure.Repositories.IRepositories
{
    public interface IUsersRepository
    {
        Task<Users> GetUserAsync(string name, string password);
        Task UpdateTokenAsync(int id, string token, DateTime tokenDateExpire);
    }
}
