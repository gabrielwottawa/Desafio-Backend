﻿using MediatR;
using MotorbikeRental.Domain.Commands.Motorbike;
using MotorbikeRental.Domain.Responses;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;

namespace MotorbikeRental.Domain.Handlers.Motorbike
{
    public class DeleteMotorbikeHandler : IRequestHandler<DeleteMotorbikeCommand, CommandResult>
    {
        private readonly IMotorbikeRepository _motorbikeRepository;

        public DeleteMotorbikeHandler(IMotorbikeRepository motorbikeRepository)
        {
            _motorbikeRepository = motorbikeRepository;
        }

        public async Task<CommandResult> Handle(DeleteMotorbikeCommand request, CancellationToken cancellationToken)
        {
            var motorbike = await _motorbikeRepository.GetMotorbikeById(request.Id)
                ?? throw new ApplicationException($"Não existe moto cadastrada com o id {request.Id} informado.");

            //validar sem a moto não tem registro de locação

            await _motorbikeRepository.DeleteMotorbikeById(request.Id);

            return new CommandResult { Message = "Moto deleta com sucesso.", Data = null };
        }
    }
}