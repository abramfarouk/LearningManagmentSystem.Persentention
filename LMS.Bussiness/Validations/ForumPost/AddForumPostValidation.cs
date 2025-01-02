using FluentValidation;
using LMS.Bussiness.DTOS.ForumPostDtos;

namespace LMS.Bussiness.Validations.ForumPost
{
    public class AddForumPostValidation : AbstractValidator<AddForumPostRequest>
    {
        public AddForumPostValidation()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {

        }

    }
}
