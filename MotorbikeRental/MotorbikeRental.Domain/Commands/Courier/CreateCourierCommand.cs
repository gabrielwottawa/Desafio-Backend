using MediatR;
using MotorbikeRental.Domain.Responses;

namespace MotorbikeRental.Domain.Commands.Courier
{
    public class CreateCourierCommand : IRequest<CommandResult>
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string RegisterNumber { get; set; }
        public string RegisterType { get; set; }
    }
}