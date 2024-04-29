using MediatR;
using MotorbikeRental.Domain.Responses;

namespace MotorbikeRental.Domain.Commands.MotorbikeRental
{
    public class GetTotalRentalValueCommand : IRequest<CommandResult>
    {
        public string MotorbikePlate { get; set; }
        public string CourierCnpj { get; set; }
        public string CourierRegisterNumber { get; set; }
        public DateTime EndDate { get; set; }
    }
}