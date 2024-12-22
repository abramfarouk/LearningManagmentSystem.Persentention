using FluentValidation;
using LMS.Bussiness.DTOS.NotificationDtos;

namespace LMS.Bussiness.Validations.Notification
{
    public class UpdateNotificationValidation : AbstractValidator<UpdateNotificationRequest>
    {
        public UpdateNotificationValidation()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.NotificationId).NotEmpty().WithMessage("NotificationId Can't be Empty")
            .NotNull().WithMessage("NotificationId is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId Can't be Empty")
            .NotNull().WithMessage("UserId is required"); ;
        }
    }
}
