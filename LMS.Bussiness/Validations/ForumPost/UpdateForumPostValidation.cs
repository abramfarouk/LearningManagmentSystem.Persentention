using FluentValidation;
using LMS.Bussiness.DTOS.ForumPostDtos;

namespace LMS.Bussiness.Validations.ForumPost
{
    public class UpdateForumPostValidation : AbstractValidator<UpdateForumPostRequest>
    {
        public UpdateForumPostValidation()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {

        }

    }

}
