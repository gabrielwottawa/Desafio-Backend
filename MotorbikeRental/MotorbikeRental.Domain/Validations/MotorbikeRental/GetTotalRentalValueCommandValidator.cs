using FluentValidation;
using MotorbikeRental.Domain.Commands.MotorbikeRental;
using MotorbikeRental.Domain.Extensions;

namespace MotorbikeRental.Domain.Validations.MotorbikeRental
{
    public class GetTotalRentalValueCommandValidator : AbstractValidator<GetTotalRentalValueCommand>
    {
        public GetTotalRentalValueCommandValidator()
        {
            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("A data de fim do aluguel deve ser fornecida.");

            RuleFor(x => x.EndDate < DateTime.Now)
                .Equal(false)
                .WithMessage("A data de fim é menor que a data atual.");

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
        }
    }
}