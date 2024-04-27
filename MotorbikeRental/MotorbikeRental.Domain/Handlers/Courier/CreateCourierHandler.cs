using MediatR;
using MotorbikeRental.Domain.Commands.Courier;
using MotorbikeRental.Domain.Extensions;
using MotorbikeRental.Domain.Responses;
using MotorbikeRental.Domain.Validations;
using MotorbikeRental.Domain.Validations.Courier;
using MotorbikeRental.Infrastructure.Models;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;

namespace MotorbikeRental.Domain.Handlers.Courier
{
    public class CreateCourierHandler : IRequestHandler<CreateCourierCommand, CommandResult>
    {
        private readonly ICouriersRepository _couriersRepository;
        private readonly IRegisterTypeRepository _registerTypeRepository;
        private readonly Validator<CreateCourierCommand> _validator = new(new CreateCourierCommandValidator());

        public CreateCourierHandler(ICouriersRepository couriersRepository
                                    , IRegisterTypeRepository registerTypeRepository)
        {
            _couriersRepository = couriersRepository;
            _registerTypeRepository = registerTypeRepository;
        }

        public async Task<CommandResult> Handle(CreateCourierCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAsync(request, cancellationToken);

            var courier = await _couriersRepository.GetCourierByCnpj(request.Cnpj, request.RegisterNumber);

            if (courier != null)
                throw new ApplicationException($"Já existe um entregador cadastrado com esse CNPJ: '{request.Cnpj}' e CNH: '{request.RegisterNumber}'");

            var registerType = await _registerTypeRepository.GetRegisterTypeByType(request.RegisterType)
                                    ?? throw new ApplicationException($"Não existe o tipo de CNH {request.RegisterType}, as opções disponíveis são A, B ou AB.");

            var newCourier = new Couriers
            {
                Name = request.Name,
                Cnpj = request.Cnpj.RemoveSpecialCharacters(),
                DateOfBirth = request.DateOfBirth,
                RegisterNumber = request.RegisterNumber,
                RegisterTypeId = registerType.Id
            };

            await _couriersRepository.InsertCourier(newCourier);

            return new CommandResult { Message = "Entregador cadastrado com sucesso.", Data = null };
        }
    }
}