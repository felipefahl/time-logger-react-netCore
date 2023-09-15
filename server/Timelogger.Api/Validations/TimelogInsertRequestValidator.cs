namespace Timelogger.Api.Validations
{
    
    public class TimelogInsertRequestValidator : AbstractValidator<TimelogInsertRequestDto> 
    {
        public static void Validate(TimelogInsertRequestDto timelogInsertRequestDto){
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