using FluentValidation;
using MotorbikeRental.Domain.Exceptions;

namespace MotorbikeRental.Domain.Validations
{
    public class Validator<T>
    {
        private readonly AbstractValidator<T> _validator;

        public Validator(AbstractValidator<T> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task ValidateAsync(T request, CancellationToken cancellationToken = default)
        {
            var resultValidator = await _validator.ValidateAsync(request, cancellationToken);

            if (!resultValidator.IsValid)
            {
                var errors = resultValidator.Errors.Select(el => el.ErrorMessage).ToList();
                throw new ListExceptions(errors);
            }
        }
    }
}