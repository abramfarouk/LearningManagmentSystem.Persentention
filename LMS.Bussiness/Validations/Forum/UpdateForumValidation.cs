using FluentValidation;
using LMS.Bussiness.DTOS.FormsDtos;

namespace LMS.Bussiness.Validations.Forum
{
    public class UpdateForumValidation : AbstractValidator<UpdateForumRequest>
    {
        public UpdateForumValidation()
        {
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {

        }
    }
}
