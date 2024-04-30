using FluentValidation;
using MotorbikeRental.Domain.Commands.Courier;
using MotorbikeRental.Domain.Extensions;

namespace MotorbikeRental.Domain.Validations.Courier
{
    public class PostDocumentCourierCommandValidator : AbstractValidator<PostDocumentCourierCommand>
    {
        public PostDocumentCourierCommandValidator()
        {
            RuleFor(x => x.Cnpj)
                .NotEmpty()
                .WithMessage("O CNPJ deve ser preenchido.");

            RuleFor(x => x.Cnpj.RemoveSpecialCharacters().Length != 14)
                .Equal(false)
                .WithMessage("O cnpj deve possuir 14 números para ser válido.");

            RuleFor(x => x.Cnpj.Distinct().Count() == 1)
                .Equal(false)
                .WithMessage("O cnpj não pode possuir todos os números iguais.");

            RuleFor(x => x.RegisterNumber)
                .NotEmpty()
                .WithMessage("O número da CNH deve ser preenchido.");

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