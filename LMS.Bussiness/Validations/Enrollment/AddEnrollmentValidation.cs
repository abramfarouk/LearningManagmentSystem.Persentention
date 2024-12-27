using FluentValidation;
using LMS.Bussiness.DTOS.EnrollmentDtos;

namespace LMS.Bussiness.Validations.Enrollment
{
    public class AddEnrollmentValidation : AbstractValidator<AddEnrollmentRequest>
    {
        public AddEnrollmentValidation()
        {
            RuleFor(x => x.CourseId).NotNull().WithMessage("CourseId Can't be Empty")
                .NotEmpty().WithMessage("CourseId is required");
            RuleFor(x => x.UserId).NotNull().WithMessage("UserId is required")
                .NotEmpty().WithMessage("UserId Can't be Empty");
        }
    }
}
