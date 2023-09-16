using FluentValidation;
using System.Linq;

namespace Timelogger.Api.Helpers
{
    public static class ValidatorHelper
    {
        public static void Validate<T>(IValidator<T> validator, T model)
        {
            var validationResult = validator.Validate(model);

            var validationErrors = validationResult?.Errors;

            if (validationErrors.Any())
                throw new ValidationException(validationErrors);
        }
    }
}
