using FluentValidation;
using LMS.Bussiness.DTOS.LessonDtos;

namespace LMS.Bussiness.Validations.Lesson
{
    public class AddLessonValidation : AbstractValidator<AddLessonRequest>
    {
        public AddLessonValidation()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {

            RuleFor(x => x.ModuleId).NotNull().WithMessage("Module Id is required")
                .NotEmpty().WithMessage("Module Id Can't be Empty");
        }
    }
}
