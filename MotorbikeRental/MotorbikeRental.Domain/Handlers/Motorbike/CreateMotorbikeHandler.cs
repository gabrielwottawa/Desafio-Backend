using MediatR;
using MotorbikeRental.Domain.Commands.Motorbike;
using MotorbikeRental.Domain.Exceptions;
using MotorbikeRental.Domain.Responses;
using MotorbikeRental.Domain.Validations;
using MotorbikeRental.Domain.Validations.Motorbike;
using MotorbikeRental.Infrastructure.Models;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;

namespace MotorbikeRental.Domain.Handlers.Motorbike
{
    public class CreateMotorbikeHandler : IRequestHandler<CreateMotorbikeCommand, CommandResult>
    {
        private readonly IMotorbikeRepository _motorbikeRepository;
        private readonly Validator<CreateMotorbikeCommand> _validator = new(new CreateMotorbikeCommandValidator());

        public CreateMotorbikeHandler(IMotorbikeRepository motorbikeRepository)
        {
            _motorbikeRepository = motorbikeRepository;
        }

        public async Task<CommandResult> Handle(CreateMotorbikeCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAsync(request, cancellationToken);

            var plate = await _motorbikeRepository.GetMotorbikeByPlateAsync(request.Plate);

            if (plate != null)
                throw new ApplicationException("Já existe uma moto com a placa informada.");

            var newMotorbike = new Motorbikes
            {
                Plate = request.Plate,
                Year = request.Year,
                Type = request.Type
            };

            await _motorbikeRepository.InsertMotorbikeAsync(newMotorbike);

            return new CommandResult { Message = $"Moto cadastrada com sucesso. Placa {request.Plate}", Data = null };
        }
    }
}