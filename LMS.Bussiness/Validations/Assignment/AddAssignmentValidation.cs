using FluentValidation;
using LMS.Bussiness.DTOS.AssignmentDtos;

namespace LMS.Bussiness.Validations.Assignment
{
    public class AddAssignmentValidation : AbstractValidator<AddAssignmentRequest>
    {
        public AddAssignmentValidation()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Title).NotNull().WithMessage("The Title Is Required")
                .NotEmpty().WithMessage("The Title Can't Be Empty");
            RuleFor
                (x => x.CourseId).NotNull().WithMessage("CourseId Is Required")
                .NotEmpty().WithMessage("CourseId Can't Be Empty");
        }
    }
}
