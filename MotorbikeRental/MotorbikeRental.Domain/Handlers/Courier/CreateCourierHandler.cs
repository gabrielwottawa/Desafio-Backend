using MediatR;
using MotorbikeRental.Domain.Commands.Courier;
using MotorbikeRental.Domain.Responses;
using MotorbikeRental.Infrastructure.Models;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;

namespace MotorbikeRental.Domain.Handlers.Courier
{
    public class CreateCourierHandler : IRequestHandler<CreateCourierCommand, CommandResult>
    {
        private readonly ICouriersRepository _couriersRepository;
        private readonly IRegisterTypeRepository _registerTypeRepository;

        public CreateCourierHandler(ICouriersRepository couriersRepository
                                    , IRegisterTypeRepository registerTypeRepository)
        {
            _couriersRepository = couriersRepository;
            _registerTypeRepository = registerTypeRepository;
        }

        public async Task<CommandResult> Handle(CreateCourierCommand request, CancellationToken cancellationToken)
        {
            var courier = await _couriersRepository.GetCourierByCnpj(request.Cnpj, request.RegisterNumber);

            if (courier != null)
                throw new ApplicationException($"Já existe um entregador cadastrado com esse CNPJ: '{request.Cnpj}' e CNH: '{request.RegisterNumber}'");

            var registerType = await _registerTypeRepository.GetRegisterTypeByType(request.RegisterType)
                                    ?? throw new ApplicationException($"Não existe o tipo de CNH, as opções disponíveis são A, B ou AB.");

            var newCourier = new Couriers
            {
                Name = request.Name,
                Cnpj = request.Cnpj,
                DateOfBirth = request.DateOfBirth,
                RegisterNumber = request.RegisterNumber,
                RegisterTypeId = registerType.Id
            };

            await _couriersRepository.InsertCourier(newCourier);

            return new CommandResult { Message = "Entregador cadastrado com sucesso.", Data = null };
        }
    }
}