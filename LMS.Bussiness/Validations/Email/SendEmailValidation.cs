using FluentValidation;
using LMS.Bussiness.DTOS.EmailDtos;

namespace LMS.Bussiness.Validations.Email
{
    public class SendEmailValidation : AbstractValidator<SendEmailDto>
    {
        public SendEmailValidation()
        {
            SendEmailValidationRules();
        }
        public void SendEmailValidationRules()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Email is required")
          .NotEmpty().WithMessage("Email is not empty ")
          .EmailAddress().WithMessage("Email is not valid");
        }
    }
}
