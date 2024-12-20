using FluentValidation;
using LMS.Bussiness.DTOS.ModuleDtos;

namespace LMS.Bussiness.Validations.Module
{
    public class UpdatedModuleValidation : AbstractValidator<UpdatedModuleRequest>
    {
        public UpdatedModuleValidation()
        {

            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {

        }

    }
}