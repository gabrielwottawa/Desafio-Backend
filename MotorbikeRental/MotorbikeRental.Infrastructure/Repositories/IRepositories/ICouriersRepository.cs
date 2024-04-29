using MotorbikeRental.Infrastructure.Models;

namespace MotorbikeRental.Infrastructure.Repositories.IRepositories
{
    public interface ICouriersRepository
    {
        Task<Couriers> GetCourierByCnpjAndRegisterNumberAsync(string cnpj, string registerNumber);
        Task InsertCourierAsync(Couriers courier);
        Task InsertUrlImageAsync(int id, string urlImage);
        void InsertCourier(Couriers courier);
    }
}
