using FluentValidation;
using LMS.Bussiness.DTOS.NotificationDtos;

namespace LMS.Bussiness.Validations.Notification
{
    public class AddNotificationValidation : AbstractValidator<AddNotificationRequest>
    {

        public AddNotificationValidation()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId Can't be Empty")
            .NotNull().WithMessage("UserId is required"); ;
        }
    }
}
