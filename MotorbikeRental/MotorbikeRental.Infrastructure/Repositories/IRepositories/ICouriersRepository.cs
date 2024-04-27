using MotorbikeRental.Infrastructure.Models;

namespace MotorbikeRental.Infrastructure.Repositories.IRepositories
{
    public interface ICouriersRepository
    {
        Task<Couriers> GetCourierByCnpj(string cnpj, string registerNumber);
        Task InsertCourier(Couriers courier);
        Task InsertUrlImage(int id, string urlImage);
    }
}
