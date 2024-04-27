using FluentValidation;
using MotorbikeRental.Domain.Commands.MotorbikeRental;
using MotorbikeRental.Domain.Extensions;

namespace MotorbikeRental.Domain.Validations.MotorbikeRental
{
    public class CreateMotorbikeRentalCommandValidator : AbstractValidator<CreateMotorbikeRentalCommand>
    {
        public CreateMotorbikeRentalCommandValidator() 
        {
            RuleFor(x => x.MotorbikePlate)
                .NotEmpty()
                .WithMessage("A placa da moto para aluguel deve ser fornecida.");

            RuleFor(x => x.CourierCnpj)
                .NotEmpty()
                .WithMessage("O CNPJ do entregador deve ser fornecido.");

            RuleFor(x => x.CourierCnpj)
                .NotEmpty()
                .WithMessage("O CNPJ deve ser preenchido.");

            RuleFor(x => x.CourierCnpj.RemoveSpecialCharacters().Length != 14)
                .Equal(false)
                .WithMessage("O cnpj deve possuir 14 números para ser válido.");

            RuleFor(x => x.CourierCnpj.Distinct().Count() == 1)
                .Equal(false)
                .WithMessage("O cnpj não pode possuir todos os números iguais.");

            RuleFor(x => x.CourierRegisterNumber)
                .NotEmpty()
                .WithMessage("A CNH do entregador deve ser fornecido.");

            RuleFor(x => x.EstimatedEndDate)
                .NotEmpty()
                .WithMessage("A data de estimativa de término do aluguel deve ser fornecida.");

            RuleFor(x => x.EstimatedEndDate < DateTime.Now)
                .Equal(false)
                .WithMessage("A data de estimativa de término é menor que a data atual.");

            RuleFor(x => x.EstimatedEndDate <= DateTime.Now.AddDays(1))
                .Equal(false)
                .WithMessage("A data de estimativa de término é menor que a data de inicio do aluguel.");

            RuleFor(x => x.RentalPlansId)
                .NotNull()
                .WithMessage("O id do plano escolhido deve ser fornecido.");
        }
    }
}