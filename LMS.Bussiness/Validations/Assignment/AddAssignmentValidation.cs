using FluentValidation;
using LMS.Bussiness.DTOS.AssignmentDtos;

namespace LMS.Bussiness.Validations.Assignment
{
    public class AddAssignmentValidation : AbstractValidator<AddAssignmentRequest>
    {
        public AddAssignmentValidation()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {

        }
    }
}
