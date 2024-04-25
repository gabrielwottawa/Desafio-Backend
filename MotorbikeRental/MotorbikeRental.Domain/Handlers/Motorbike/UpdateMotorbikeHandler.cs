﻿using MediatR;
using MotorbikeRental.Domain.Commands.Motorbike;
using MotorbikeRental.Domain.Responses;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;

namespace MotorbikeRental.Domain.Handlers.Motorbike
{
    public class UpdateMotorbikeHandler : IRequestHandler<UpdateMotorbikeCommand, CommandResult>
    {
        private readonly IMotorbikeRepository _motorbikeRepository;

        public UpdateMotorbikeHandler(IMotorbikeRepository motorbikeRepository)
        {
            _motorbikeRepository = motorbikeRepository;
        }

        public async Task<CommandResult> Handle(UpdateMotorbikeCommand request, CancellationToken cancellationToken)
        {
            var motorbike = await _motorbikeRepository.GetMotorbikeById(request.Id);

            if (motorbike == null) 
                throw new ApplicationException($"Não existe moto cadastrada com o id {request.Id} informado.");

            if (motorbike.Plate == request.Plate)
                throw new ApplicationException($"A placa informada é a mesma já cadastrada para essa moto.");

            var existOtherMotorbike = await _motorbikeRepository.GetMotorbikeByPlate(request.Plate);

            if (existOtherMotorbike != null && !existOtherMotorbike.Id.Equals(motorbike.Id))
                throw new ApplicationException($"A placa informada é a mesma já cadastrada para outra moto.");

            await _motorbikeRepository.UpdateMotorbike(request.Id, request.Plate);

            return new CommandResult { Message = $"Moto atualiza com sucesso. Nova placa '{request.Plate}'", Data = null };
        }
    }
}