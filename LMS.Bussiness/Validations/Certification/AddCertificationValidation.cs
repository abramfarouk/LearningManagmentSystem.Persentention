using FluentValidation;
using LMS.Bussiness.DTOS.CertificateDtos;

namespace LMS.Bussiness.Validations.Certification
{
    internal class AddCertificationValidation : AbstractValidator<AddCeritificationRequest>
    {
        public AddCertificationValidation()
        {
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Crs_Id).NotEmpty().WithMessage("Course_Id can't be Empty")
                 .NotNull().WithMessage("Crs_Id Is Required ");
            RuleFor(x => x.Std_Id).NotNull().WithMessage("Student_Id Is Required ")
             .NotEmpty().WithMessage("Student_Id can't be Empty ");

        }
    }
}
