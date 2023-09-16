using FluentValidation;
using Timelogger.Api.Dtos;
using Timelogger.Api.Helpers;

namespace Timelogger.Api.Validations
{
    
    public class TimelogInsertRequestValidator : AbstractValidator<TimelogInsertRequestDto> 
    {
        public static new void Validate(TimelogInsertRequestDto timelogInsertRequestDto){
            var validator = new TimelogInsertRequestValidator();

            ValidatorHelper.Validate(validator, timelogInsertRequestDto);
        }

        public TimelogInsertRequestValidator() 
        {
            RuleFor(x => x.Note).MaximumLength(100);
            RuleFor(x => x.DurationMinutes).GreaterThan(30);
        }
    }
}