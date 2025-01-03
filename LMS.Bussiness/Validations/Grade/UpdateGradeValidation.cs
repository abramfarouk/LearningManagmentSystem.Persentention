using FluentValidation;
using LMS.Bussiness.DTOS.GradeDtos;

namespace LMS.Bussiness.Validations.Grade
{
    public class UpdateGradeValidation : AbstractValidator<UpdateGradeRequest>
    {
        public UpdateGradeValidation()
        {
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("GradeId Can't Be Empty")
                .NotNull().WithMessage("Id is required");
            RuleFor(x => x.grade).NotEmpty().WithMessage("Grade is required");
            RuleFor(x => x.grade).InclusiveBetween(0, 100).WithMessage("Grade must be between 0 and 100");
            RuleFor(x => x.SubmissionId).NotEmpty().WithMessage("SubmissionId Can't Be Empty")
                .NotNull().WithMessage("SubmissionId is required");
        }

    }

}
