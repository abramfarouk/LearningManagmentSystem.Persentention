using FluentValidation;
using LMS.Bussiness.DTOS.UserDto;

namespace LMS.Bussiness.Validations.Users
{
    public class AddUserValidation : AbstractValidator<AddUserRequest>
    {

        public AddUserValidation()
        {
            ApplyValidatorRelus();
        }

        public void ApplyValidatorRelus()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("{PropertyName} is Not Empty")
            .NotNull().WithMessage("{PropertyName} is Not Null");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("{PropertyName} is Not Empty")
           .NotNull().WithMessage("{PropertyName} is Not Null");
            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} is Not Empty")
                .NotNull().WithMessage("{PropertyName} is Not Null");
            //.Matches(@"^[a-zA-Z]{3,}[a-zA-Z._0-9]{0,20}[a-zA-Z0-9]{1,}(@gmail.com|@yahoo.com)$")
            //.WithMessage("Email must start with at least three alphabetic characters,allowed only '._' and numbers. Example :user123@(gmail.com|yahoo.com)");

            RuleFor(x => x.PhoneNumber)
           .Matches(@"^(?:010|011|012|015)[0-9]{8}$")
           .WithMessage("Phone number must start with 010, 011,015 or 012 and be followed by 8 digits.");

            RuleFor(x => x.Password)
        .NotEmpty().WithMessage("{PropertyName} is Not Empty")
        .NotNull().WithMessage("{PropertyName} is Not Null");

            RuleFor(x => x.ConfirmPassword)
           .NotEmpty().WithMessage("{PropertyName} is Not Empty")
           .NotNull().WithMessage("{PropertyName} is Not Null")
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match.Try again !");


            RuleFor(x => x.Address)
      .NotEmpty().WithMessage("{PropertyName} is Not Empty")
      .NotNull().WithMessage("{PropertyName} is Not Null");

            RuleFor(x => x.City)
      .NotEmpty().WithMessage("{PropertyName} is Not Empty")
      .NotNull().WithMessage("{PropertyName} is Not Null");

            RuleFor(x => x.Country)
      .NotEmpty().WithMessage("{PropertyName} is Not Empty")
      .NotNull().WithMessage("{PropertyName} is Not Null");

        }
    }

}
