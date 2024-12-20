using FluentValidation;
using LMS.Bussiness.DTOS.AssignmentDtos;

namespace LMS.Bussiness.Validations.Module
{
    public class AddModuleValidation : AbstractValidator<AddAssignmentRequest>
    {
        public AddModuleValidation()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {

        }

    }

}
