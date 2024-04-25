using MotorbikeRental.Infrastructure.Models;

namespace MotorbikeRental.Services.Auth
{
    public interface IAuthService
    {
        string Authenticate(Users users);
    }
}