using FluentValidation;
using LMS.Bussiness.DTOS.GradeDtos;

namespace LMS.Bussiness.Validations.Grade
{
    public class AddGradeValidation : AbstractValidator<AddGradeRequest>
    {
        public AddGradeValidation()
        {
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.grade).NotEmpty().WithMessage("Grade is required");
            RuleFor(x => x.grade).InclusiveBetween(0, 100).WithMessage("Grade must be between 0 and 100");
            RuleFor(x => x.SubmissionId).NotEmpty().WithMessage("SubmissionId Can't Be Empty")
                .NotNull().WithMessage("Submission Id is required");
        }

    }
}
