using MediatR;
using MotorbikeRental.Domain.Responses;

namespace MotorbikeRental.Domain.Commands.Auth
{
    public class AuthCommand : IRequest<CommandResult>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}