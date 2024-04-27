using FluentValidation;
using MotorbikeRental.Domain.Commands.Courier;
using MotorbikeRental.Domain.Extensions;

namespace MotorbikeRental.Domain.Validations.Courier
{
    public class PostDocumentCourierCommandValidator : AbstractValidator<PostDocumentCourierCommand>
    {
        public PostDocumentCourierCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("O id deve ser informado.");

            RuleFor(x => x.FileName)
                .NotEmpty()
                .WithMessage("O nome do arquivo deve ser informado.");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("O arquivo está sem conteúdo.");

            RuleFor(x => x.FileName.ValidateArchiveExtension(new List<string> { ".png", ".bmp" }))
                .Equal(false)
                .WithMessage("O arquivo deve ter extensão .png ou .bmp");

            RuleFor(x => x.Content.IsBase64String())
                .Equal(true)
                .WithMessage("O conteúdo não é uma cadeia Base-64 válida.");
        }
    }
}