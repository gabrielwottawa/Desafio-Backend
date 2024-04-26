using FluentValidation;
using MotorbikeRental.Domain.Commands.Motorbike;

namespace MotorbikeRental.Domain.Validations.Motorbike
{
    public class CreateMotorbikeCommandValidator : AbstractValidator<CreateMotorbikeCommand>
    {
        public CreateMotorbikeCommandValidator()
        {
            RuleFor(x => x.Plate)
                .NotEmpty()
                .WithMessage("A placa da moto deve ser preenchida.");

            RuleFor(x => x.Year)
                .NotEmpty()
                .WithMessage("O ano da moto deve ser informado.");

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("O modelo da moto deve ser informado.");
        }
    }
}