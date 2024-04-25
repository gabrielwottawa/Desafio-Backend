using MediatR;
using MotorbikeRental.Domain.Commands.Motorbike;
using MotorbikeRental.Domain.Responses;
using MotorbikeRental.Infrastructure.Models;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;

namespace MotorbikeRental.Domain.Handlers.Motorbike
{
    public class CreateMotorbikeHandler : IRequestHandler<CreateMotorbikeCommand, CommandResult>
    {
        private readonly IMotorbikeRepository _motorbikeRepository;

        public CreateMotorbikeHandler(IMotorbikeRepository motorbikeRepository)
        {
            _motorbikeRepository = motorbikeRepository;
        }

        public async Task<CommandResult> Handle(CreateMotorbikeCommand request, CancellationToken cancellationToken)
        {
            var plate = await _motorbikeRepository.GetMotorbikeByPlate(request.Plate);

            if (plate != null)
                throw new ApplicationException("Já existe uma moto com a placa informada.");

            var newMotorbike = new Motorbikes
            {
                Plate = request.Plate,
                Year = request.Year,
                Type = request.Type
            };

            await _motorbikeRepository.InsertMotorbike(newMotorbike);

            return new CommandResult { Message = $"Moto cadastrada com sucesso. Placa {request.Plate}", Data = null };
        }
    }
}