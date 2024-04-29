using MediatR;
using Microsoft.Extensions.Configuration;
using MotorbikeRental.Domain.Commands.MotorbikeRental;
using MotorbikeRental.Domain.Enum;
using MotorbikeRental.Domain.Extensions;
using MotorbikeRental.Domain.Responses;
using MotorbikeRental.Domain.Validations;
using MotorbikeRental.Domain.Validations.MotorbikeRental;
using MotorbikeRental.Infrastructure.Models;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;
using MotorbikeRental.Services.RabbitMq;

namespace MotorbikeRental.Domain.Handlers.MotorbikeRental
{
    public class MotorbikeRentalHandler : IRequestHandler<CreateMotorbikeRentalCommand, CommandResult>
    {
        private readonly ICouriersRepository _couriersRepository;
        private readonly IMotorbikeRepository _motorbikeRepository;
        private readonly IMotorbikeRentalRepository _motorbikeRentalRepository;
        private readonly IRentalPlansRepository _rentalPlansRepository;
        private readonly IRegisterTypeRepository _registerRegisterTypeRepository;
        private readonly IConfiguration _configuration;
        private readonly Validator<CreateMotorbikeRentalCommand> _validator = new(new CreateMotorbikeRentalCommandValidator());

        public MotorbikeRentalHandler(ICouriersRepository couriersRepository
                                        , IMotorbikeRepository motorbikeRepository
                                        , IMotorbikeRentalRepository motorbikeRentalRepository
                                        , IRentalPlansRepository rentalPlansRepository
                                        , IRegisterTypeRepository registerRegisterTypeRepository
                                        , IConfiguration configuration)
        {
            _couriersRepository = couriersRepository;
            _motorbikeRepository = motorbikeRepository;
            _motorbikeRentalRepository = motorbikeRentalRepository;
            _rentalPlansRepository = rentalPlansRepository;
            _registerRegisterTypeRepository = registerRegisterTypeRepository;
            _configuration = configuration;
        }

        public async Task<CommandResult> Handle(CreateMotorbikeRentalCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAsync(request, cancellationToken);

            var motorbike = await _motorbikeRepository.GetMotorbikeByPlateAsync(request.MotorbikePlate)
                               ?? throw new ApplicationException($"Não existe moto com a placa '{request.MotorbikePlate}' fornecidos.");

            var courier = await _couriersRepository.GetCourierByCnpjAndRegisterNumberAsync(request.CourierCnpj.RemoveSpecialCharacters(), request.CourierRegisterNumber)
                               ?? throw new ApplicationException($"Não existe entregador com o CNPJ '{request.CourierCnpj}' e a CNH '{request.CourierRegisterNumber}' fornecidos.");

            if (await _motorbikeRentalRepository.IsRentedMotorbikeAsync(motorbike.Plate))
                throw new ApplicationException("Está moto está alugada.");

            var courierRegisterType = await _registerRegisterTypeRepository.GetRegisterTypeByIdAsync(courier.RegisterTypeId);

            if (courierRegisterType.Type == "B")
                throw new ApplicationException($"Este entregador não possui a CNH do tipo 'A' ou 'AB'. CNH do entregador: '{courierRegisterType.Type}'");

            var rentalPlan = await _rentalPlansRepository.GetRentalPlanByIdAsync(request.RentalPlansId) 
                               ?? throw new ApplicationException("O plano informado não existe.");

            if (rentalPlan.NumberDays < request.EstimatedEndDate.DaysDifference(DateTime.Now, 1))
                throw new ApplicationException($"A data de estimativa de término é maior que a data de fim do plano selecionado.");

            var newMotorbikeRental = new MotorbikeRentals
            {
                StartDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")),
                EndDate = Convert.ToDateTime(DateTime.Now.AddDays(rentalPlan.NumberDays).ToString("yyyy-MM-dd")),
                EstimatedEndDate = Convert.ToDateTime(request.EstimatedEndDate.ToString("yyyy-MM-dd")),
                RentalPlansId = rentalPlan.Id,
                MotorbikeId = motorbike.Id,
                MotorbikePlate = motorbike.Plate,
                CourierId = courier.Id,
                CourierCnpj = courier.Cnpj.RemoveSpecialCharacters(),
                CourierRegisterNumber = courier.RegisterNumber,
                Status = (int)MotorbikeRentalStatus.Processing
            };

            var rabbitMqService = new RabbitMqService<MotorbikeRentals>(_configuration);
            rabbitMqService.SendMessage(newMotorbikeRental, _configuration.GetSection("RabbitQueues:MotorbikeRentals").Value);

            return new CommandResult { Message = $"Aluguel da moto '{motorbike.Type}' com a placa: '{motorbike.Plate}' feito com sucesso." };
        }
    }
}