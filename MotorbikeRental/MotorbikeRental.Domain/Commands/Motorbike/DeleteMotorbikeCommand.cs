using MediatR;
using MotorbikeRental.Domain.Responses;

namespace MotorbikeRental.Domain.Commands.Motorbike
{
    public class DeleteMotorbikeCommand : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
