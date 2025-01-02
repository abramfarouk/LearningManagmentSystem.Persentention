using FluentValidation;
using LMS.Bussiness.DTOS.FormsDtos;

namespace LMS.Bussiness.Validations.Forum
{
    public class UpdateForumValidation : AbstractValidator<UpdateForumRequest>
    {
        public UpdateForumValidation()
        {
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("ForumId is required")
                .NotEmpty().WithMessage("ForumId Can't Be Empty");
            RuleFor(x => x.CourseId).NotNull().WithMessage("CourseId is required")
         .NotEmpty().WithMessage("CourseId Can't Be Empty");
        }
    }
}
