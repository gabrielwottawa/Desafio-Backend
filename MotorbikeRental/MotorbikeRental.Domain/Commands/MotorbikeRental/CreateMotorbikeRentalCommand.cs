using MediatR;
using MotorbikeRental.Domain.Responses;

namespace MotorbikeRental.Domain.Commands.MotorbikeRental
{
    public class CreateMotorbikeRentalCommand : IRequest<CommandResult>
    {
        public string MotorbikePlate { get; set; }
        public string CourierCnpj { get; set; }
        public string CourierRegisterNumber { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public int RentalPlansId { get; set; }
    }
}