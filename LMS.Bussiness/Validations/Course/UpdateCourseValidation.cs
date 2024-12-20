using FluentValidation;
using LMS.Bussiness.DTOS.CourseDtos;

namespace LMS.Bussiness.Validations.Course
{
    public class UpdateCourseValidation : AbstractValidator<UpdateCourseRequest>
    {
        public UpdateCourseValidation()
        {
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Crs_Id).NotNull().WithMessage("Course Id is required")
                .NotEmpty().WithMessage("Course Id can't Empty");
            RuleFor(x => x.Title).NotNull().WithMessage("Title is required")
                .MaximumLength(50).WithMessage("Title can not be more than 50 characters")
                .NotEmpty().WithMessage("Title can't Empty");
        }
    }
}
