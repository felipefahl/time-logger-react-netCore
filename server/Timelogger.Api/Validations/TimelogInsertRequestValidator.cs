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
            RuleFor(x => x.Note).MaximumLength(100).WithMessage("Can not be greater than 100 character");
            RuleFor(x => x.DurationMinutes).GreaterThanOrEqualTo(30).WithMessage("Must be greater then or equal to 30");
        }
    }
}