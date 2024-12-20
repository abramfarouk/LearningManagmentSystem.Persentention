using FluentValidation;
using LMS.Bussiness.DTOS.CertificateDtos;

namespace LMS.Bussiness.Validations.Certification
{
    public class UpdateCertificationValidation : AbstractValidator<UpdateCeritificationRequest>
    {
        public UpdateCertificationValidation()
        {
            ApplyValidationRules();


        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Certificated_Id).NotNull().WithMessage("Certificated_Id Is Required")
                  .NotEmpty().WithMessage("Certification_Id can't be Empty");

        }
    }
}
