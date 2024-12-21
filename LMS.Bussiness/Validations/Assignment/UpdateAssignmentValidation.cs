using FluentValidation;
using LMS.Bussiness.DTOS.AssignmentDtos;

namespace LMS.Bussiness.Validations.Assignment
{
    public class UpdateAssignmentValidation : AbstractValidator<UpdatedAssignmentRequest>
    {
        public UpdateAssignmentValidation()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {

            RuleFor(x => x.AssignmentId).NotNull().WithMessage("AssignmentId Is Required")
           .NotEmpty().WithMessage("AssignmentId Can't Be Empty");
            RuleFor(x => x.Title).NotNull().WithMessage("The Title Is Required")
           .NotEmpty().WithMessage("The Title Can't Be Empty");
            RuleFor(x => x.CourseId).NotNull().WithMessage("CourseId Is Required")
           .NotEmpty().WithMessage("CourseId Can't Be Empty");
        }

    }
}
