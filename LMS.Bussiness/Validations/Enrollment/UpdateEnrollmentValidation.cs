using FluentValidation;
using LMS.Bussiness.DTOS.EnrollmentDtos;

namespace LMS.Bussiness.Validations.Enrollment
{
    public class UpdateEnrollmentValidation : AbstractValidator<UpdateEnrollmentRequest>
    {
        public UpdateEnrollmentValidation()
        {

            ApplyValidationRules();

        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id Can't be Empty")
               .NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.CourseId).NotNull().WithMessage("CourseId Can't be Empty")
               .NotEmpty().WithMessage("CourseId is required");
            RuleFor(x => x.UserId).NotNull().WithMessage("UserId is required")
                .NotEmpty().WithMessage("UserId Can't be Empty");
        }

    }
}
