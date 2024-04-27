using FluentValidation;
using MotorbikeRental.Domain.Commands.Courier;
using MotorbikeRental.Domain.Extensions;

namespace MotorbikeRental.Domain.Validations.Courier
{
    public class CreateCourierCommandValidator : AbstractValidator<CreateCourierCommand>
    {
        public CreateCourierCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome do entregador deve ser preenchido.");

            RuleFor(x => x.Cnpj)
                .NotEmpty()
                .WithMessage("O CNPJ deve ser preenchido.");

            RuleFor(x => x.Cnpj.RemoveSpecialCharacters().Length != 14)
                .Equal(false)
                .WithMessage("O cnpj deve possuir 14 números para ser válido.");

            RuleFor(x => x.Cnpj.Distinct().Count() == 1)
                .Equal(false)
                .WithMessage("O cnpj não pode possuir todos os números iguais.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .WithMessage("A data de nascimento deve ser preenchida.");

            RuleFor(x => x.RegisterNumber)
                .NotEmpty()
                .WithMessage("O número da CNH deve ser preenchido.");

            RuleFor(x => x.RegisterType)
                .NotEmpty()
                .WithMessage("O tipo da CNH deve ser preenchido.");
        }
    }
}