using MediatR;
using MotorbikeRental.Domain.Responses;

namespace MotorbikeRental.Domain.Commands.Motorbike
{
    public class UpdateMotorbikeCommand : IRequest<CommandResult>
    {
        public int Id { get; set; }
        public string Plate {  get; set; }
    }
}