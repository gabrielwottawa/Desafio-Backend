using MediatR;
using MotorbikeRental.Domain.Commands.Motorbike;
using MotorbikeRental.Domain.Responses;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;

namespace MotorbikeRental.Domain.Handlers.Motorbike
{
    public class GetMotorbikesHandler : IRequestHandler<GetMotorbikesCommand, CommandResult>
    {
        private readonly IMotorbikeRepository _motorbikeRepository;

        public GetMotorbikesHandler(IMotorbikeRepository motorbikeRepository)
        {
            _motorbikeRepository = motorbikeRepository;
        }

        public async Task<CommandResult> Handle(GetMotorbikesCommand request, CancellationToken cancellationToken)
        {
            var motorbikes = await _motorbikeRepository.GetAllMotorbikesAsync(request.Plate);

            if (request.Plate != null)
                return new CommandResult { Message = "Motos registradas na plataforma.", Data = motorbikes };

            return new CommandResult { Message = "Motos registradas na plataforma.", Data = motorbikes.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList() };
        }
    }
}