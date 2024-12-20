using FluentValidation;
using LMS.Bussiness.DTOS.UserDto;

namespace LMS.Bussiness.Validations.Users
{
    public class UpdateUserValidation : AbstractValidator<UpdateUserRequest>
    {

        public UpdateUserValidation()
        {

        }


        public void ApplyValidationRules()
        {
            RuleFor(x => x.UserId)
          .NotNull().WithMessage("UserId is required.");

            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("FullName is required.")
                .NotEmpty().WithMessage("FullName Can't be Empty");

            RuleFor(x => x.LastName)
                .NotNull().WithMessage("UserName is required.")
                .NotEmpty().WithMessage("UserName Can't be Empty");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invaild Email.")
                .NotNull().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email Can't be Empty");

        }
    }
}
