using FluentValidation;
using LMS.Bussiness.DTOS.LessonDtos;

namespace LMS.Bussiness.Validations.Lesson
{
    public class UpdateLessonValidation : AbstractValidator<UpdateLessonRequest>
    {

        public UpdateLessonValidation()
        {

            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.LessonId).NotNull().WithMessage("Lesson Id is required")
                  .NotEmpty().WithMessage("Lesson Id Can't be Empty");
            RuleFor(x => x.ModuleId).NotNull().WithMessage("Module Id is required")
                .NotEmpty().WithMessage("Module Id Can't be Empty");

        }
    }
}
