using MediatR;

namespace MotorbikeRental.Domain.Commands.Auth
{
    public class AuthCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}