using MotorbikeRental.Infrastructure.Models;

namespace MotorbikeRental.Infrastructure.Repositories.IRepositories
{
    public interface IRentalPlansRepository
    {
        Task<IEnumerable<RentalPlans>> GetAllRentalPlans();
        Task<RentalPlans> GetRentalPlanById(int id);
    }
}