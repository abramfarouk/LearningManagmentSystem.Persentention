using FluentValidation;
using LMS.Bussiness.DTOS.FormsDtos;

namespace LMS.Bussiness.Validations.Forum
{
    public class AddForumValidation : AbstractValidator<AddForumRequest>
    {
        public AddForumValidation()
        {
            ApplyValidationRules();


        }
        public void ApplyValidationRules()
        {

        }
    }
}
