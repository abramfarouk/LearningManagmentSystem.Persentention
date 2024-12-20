using FluentValidation;
using LMS.Bussiness.DTOS.CourseDtos;

namespace LMS.Bussiness.Validations.Course
{
    public class AddCourseValidation : AbstractValidator<AddCourseRequest>
    {
        public AddCourseValidation()
        {
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Title).NotNull().WithMessage("Title is required")
                .MaximumLength(50).WithMessage("Title can not be more than 50 characters")
                .NotEmpty().WithMessage("Title can't Empty");
            RuleFor(x => x.TeacherId)
    .NotNull().WithMessage("TeacherId is required.")
    .NotEmpty().WithMessage("TeacherId Can't be Empty");

        }

    }

}
