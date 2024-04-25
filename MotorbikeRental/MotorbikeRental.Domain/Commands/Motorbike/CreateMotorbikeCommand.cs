using MediatR;
using MotorbikeRental.Domain.Responses;

namespace MotorbikeRental.Domain.Commands.Motorbike
{
    public class CreateMotorbikeCommand : IRequest<CommandResult>
    {
        public string Plate { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }
    }
}