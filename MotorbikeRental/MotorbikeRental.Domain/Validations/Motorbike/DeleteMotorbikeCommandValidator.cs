using FluentValidation;
using MotorbikeRental.Domain.Commands.Motorbike;

namespace MotorbikeRental.Domain.Validations.Motorbike
{
    public class DeleteMotorbikeCommandValidator : AbstractValidator<DeleteMotorbikeCommand>
    {
        public DeleteMotorbikeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("O id deve ser informado.");
        }
    }
}