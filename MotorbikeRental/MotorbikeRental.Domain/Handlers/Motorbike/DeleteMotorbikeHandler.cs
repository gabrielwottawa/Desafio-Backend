using MediatR;
using MotorbikeRental.Domain.Commands.Motorbike;
using MotorbikeRental.Domain.Responses;
using MotorbikeRental.Domain.Validations;
using MotorbikeRental.Domain.Validations.Motorbike;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;

namespace MotorbikeRental.Domain.Handlers.Motorbike
{
    public class DeleteMotorbikeHandler : IRequestHandler<DeleteMotorbikeCommand, CommandResult>
    {
        private readonly IMotorbikeRepository _motorbikeRepository;
        private readonly IMotorbikeRentalRepository _motorbikeRentalRepository;
        private readonly Validator<DeleteMotorbikeCommand> _validator = new(new DeleteMotorbikeCommandValidator());

        public DeleteMotorbikeHandler(IMotorbikeRepository motorbikeRepository
                                    , IMotorbikeRentalRepository motorbikeRentalRepository)
        {
            _motorbikeRepository = motorbikeRepository;
            _motorbikeRentalRepository = motorbikeRentalRepository;
        }

        public async Task<CommandResult> Handle(DeleteMotorbikeCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAsync(request, cancellationToken);

            var motorbike = await _motorbikeRepository.GetMotorbikeById(request.Id)
                ?? throw new ApplicationException($"Não existe moto cadastrada com o id {request.Id} informado.");

            var motorbikeExists = await _motorbikeRentalRepository.IsRentedMotorbike(motorbike.Plate);

            if (motorbikeExists)
                throw new ApplicationException($"A moto informada já possui registros de aluguel. Não pode ser deletada.");

            await _motorbikeRepository.DeleteMotorbikeById(request.Id);

            return new CommandResult { Message = "Moto deleta com sucesso.", Data = null };
        }
    }
}