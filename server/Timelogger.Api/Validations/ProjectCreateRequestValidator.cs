using FluentValidation;
using Timelogger.Api.Dtos;
using Timelogger.Api.Helpers;

namespace Timelogger.Api.Validations
{
    
    public class ProjectCreateRequestValidator : AbstractValidator<ProjectCreateRequestDto> 
    {
        public static new void Validate(ProjectCreateRequestDto projectCreateRequestDto){
            var validator = new ProjectCreateRequestValidator();

            ValidatorHelper.Validate(validator, timelogInsertRequestDto);
        }

        public ProjectCreateRequestValidator() 
        {
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("Can not be greater than 100 character");
            RuleFor(x => x.DeadLine).GreaterThanOrEqualTo(DateTime.Today()).WithMessage("Must be greater then or equal to Today");
        }
    }
}