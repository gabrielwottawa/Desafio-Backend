using MediatR;
using MotorbikeRental.Domain.Commands.Test;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;

namespace MotorbikeRental.Domain.Handlers.Test
{
    public class TestHandler : IRequestHandler<TestCommand, string>
    {
        private readonly IMotorbikeRepository _motorbikeRepository;

        public TestHandler(IMotorbikeRepository motorbikeRepository)
        {
            _motorbikeRepository = motorbikeRepository;
        }

        public async Task<string> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            var motorbikes = await _motorbikeRepository.GetAllMotorbike();
            return await Task.FromResult("Numero de registros: " + motorbikes.Count());
        }
    }
}