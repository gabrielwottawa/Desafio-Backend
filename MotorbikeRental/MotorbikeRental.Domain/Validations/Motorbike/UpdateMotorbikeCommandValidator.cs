using FluentValidation;
using MotorbikeRental.Domain.Commands.Motorbike;

namespace MotorbikeRental.Domain.Validations.Motorbike
{
    public class UpdateMotorbikeCommandValidator : AbstractValidator<UpdateMotorbikeCommand>
    {
        public UpdateMotorbikeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("O id deve ser informado.");

            RuleFor(x => x.Plate)
                .NotEmpty()
                .WithMessage("A placa da moto deve ser preenchida.");
        }
    }
}