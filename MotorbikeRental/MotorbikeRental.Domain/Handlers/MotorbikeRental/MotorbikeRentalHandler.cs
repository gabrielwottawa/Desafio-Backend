using MediatR;
using MotorbikeRental.Domain.Commands.MotorbikeRental;
using MotorbikeRental.Domain.Extensions;
using MotorbikeRental.Domain.Responses;
using MotorbikeRental.Domain.Validations;
using MotorbikeRental.Domain.Validations.MotorbikeRental;
using MotorbikeRental.Infrastructure.Models;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;

namespace MotorbikeRental.Domain.Handlers.MotorbikeRental
{
    public class MotorbikeRentalHandler : IRequestHandler<CreateMotorbikeRentalCommand, CommandResult>
    {
        private readonly ICouriersRepository _couriersRepository;
        private readonly IMotorbikeRepository _motorbikeRepository;
        private readonly IMotorbikeRentalRepository _motorbikeRentalRepository;
        private readonly IRentalPlansRepository _rentalPlansRepository;
        private readonly IRegisterTypeRepository _registerRegisterTypeRepository;
        private readonly Validator<CreateMotorbikeRentalCommand> _validator = new(new CreateMotorbikeRentalCommandValidator());

        public MotorbikeRentalHandler(ICouriersRepository couriersRepository
                                        , IMotorbikeRepository motorbikeRepository
                                        , IMotorbikeRentalRepository motorbikeRentalRepository
                                        , IRentalPlansRepository populationRepository
                                        , IRegisterTypeRepository registerRegisterTypeRepository)
        {
            _couriersRepository = couriersRepository;
            _motorbikeRepository = motorbikeRepository;
            _motorbikeRentalRepository = motorbikeRentalRepository;
            _rentalPlansRepository = populationRepository;
            _registerRegisterTypeRepository = registerRegisterTypeRepository;
        }

        public async Task<CommandResult> Handle(CreateMotorbikeRentalCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAsync(request, cancellationToken);

            var motorbike = await _motorbikeRepository.GetMotorbikeByPlate(request.MotorbikePlate)
                               ?? throw new ApplicationException($"Não existe moto com a placa {request.MotorbikePlate} fornecidos.");

            var courier = await _couriersRepository.GetCourierByCnpj(request.CourierCnpj.RemoveSpecialCharacters(), request.CourierRegisterNumber)
                               ?? throw new ApplicationException($"Não existe entregador com o CNPJ '{request.CourierCnpj}' e a CNH '{request.CourierRegisterNumber}' fornecidos.");

            if (await _motorbikeRentalRepository.MotorbikeIsRented(motorbike.Plate))
                throw new ApplicationException("Está moto está alugada.");

            var courierRegisterType = await _registerRegisterTypeRepository.GetRegisterTypeById(courier.RegisterTypeId);

            if (courierRegisterType.Type == "B")
                throw new ApplicationException($"Este entregador não possui a CNH do tipo 'A' ou 'AB'. CNH do entregador: {courierRegisterType.Type}");

            var rentalPlan = await _rentalPlansRepository.GetRentalPlanById(request.RentalPlansId) 
                               ?? throw new ApplicationException("O plano informado não existe.");

            if (rentalPlan.NumberDays < request.EstimatedEndDate.DaysDifference())
                throw new ApplicationException($"A data de estimativa de término é maior que a data de fim do plano selecionado.");

            var newMotorbikeRental = new MotorbikeRentals
            {
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(1 + rentalPlan.NumberDays),
                EstimatedEndDate = request.EstimatedEndDate,
                RentalPlansId = rentalPlan.Id,
                MotorbikeId = motorbike.Id,
                MotorbikePlate = motorbike.Plate,
                CourierId = courier.Id,
                CourierCnpj = courier.Cnpj.RemoveSpecialCharacters(),
                CourierRegisterNumber = courier.RegisterNumber
            };

            await _motorbikeRentalRepository.InsertMotorbikeRental(newMotorbikeRental);

            return new CommandResult { Message = $"Aluguel da moto '{motorbike.Type}' com a placa: {motorbike.Plate} feito com sucesso." };
        }
    }
}