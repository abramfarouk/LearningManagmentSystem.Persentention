using FluentValidation;
using LMS.Bussiness.DTOS.AssignmentDtos;

namespace LMS.Bussiness.Validations.Module
{
    public class AddModuleValidation : AbstractValidator<AddAssignmentRequest>
    {
        public AddModuleValidation()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title can't be Empty")
           .NotNull().WithMessage("Title Is required");
            RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId can't be Empty")
           .NotNull().WithMessage("CourseId Is required");

        }

    }

}
