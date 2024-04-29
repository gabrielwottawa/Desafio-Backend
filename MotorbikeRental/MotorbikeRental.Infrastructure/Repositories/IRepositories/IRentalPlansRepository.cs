using MotorbikeRental.Infrastructure.Models;

namespace MotorbikeRental.Infrastructure.Repositories.IRepositories
{
    public interface IRentalPlansRepository
    {
        Task<IEnumerable<RentalPlans>> GetAllRentalPlansAsync();
        Task<RentalPlans> GetRentalPlanByIdAsync(int id);
    }
}