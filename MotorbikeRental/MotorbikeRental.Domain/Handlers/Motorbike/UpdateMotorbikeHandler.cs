using MediatR;
using MotorbikeRental.Domain.Commands.Motorbike;
using MotorbikeRental.Domain.Exceptions;
using MotorbikeRental.Domain.Responses;
using MotorbikeRental.Domain.Validations;
using MotorbikeRental.Domain.Validations.Motorbike;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;

namespace MotorbikeRental.Domain.Handlers.Motorbike
{
    public class UpdateMotorbikeHandler : IRequestHandler<UpdateMotorbikeCommand, CommandResult>
    {
        private readonly IMotorbikeRepository _motorbikeRepository;
        private readonly Validator<UpdateMotorbikeCommand> _validator = new(new UpdateMotorbikeCommandValidator());

        public UpdateMotorbikeHandler(IMotorbikeRepository motorbikeRepository)
        {
            _motorbikeRepository = motorbikeRepository;
        }

        public async Task<CommandResult> Handle(UpdateMotorbikeCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAsync(request, cancellationToken);

            var motorbike = await _motorbikeRepository.GetMotorbikeByIdAsync(request.Id);

            if (motorbike == null) 
                throw new ApplicationException($"Não existe moto cadastrada com o id {request.Id} informado.");

            if (motorbike.Plate == request.Plate)
                throw new ApplicationException($"A placa informada é a mesma já cadastrada para essa moto.");

            var existOtherMotorbike = await _motorbikeRepository.GetMotorbikeByPlateAsync(request.Plate);

            if (existOtherMotorbike != null && !existOtherMotorbike.Id.Equals(motorbike.Id))
                throw new ApplicationException($"A placa informada é a mesma já cadastrada para outra moto.");

            await _motorbikeRepository.UpdateMotorbikeAsync(request.Id, request.Plate);

            return new CommandResult { Message = $"Moto atualiza com sucesso. Nova placa '{request.Plate}'", Data = null };
        }
    }
}