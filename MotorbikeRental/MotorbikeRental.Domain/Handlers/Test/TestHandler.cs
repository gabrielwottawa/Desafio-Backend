using MediatR;
using MotorbikeRental.Domain.Commands.Test;

namespace MotorbikeRental.Domain.Handlers.Test
{
    public class TestHandler : IRequestHandler<TestCommand, string>
    {
        public Task<string> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Chegou!!");
        }
    }
}