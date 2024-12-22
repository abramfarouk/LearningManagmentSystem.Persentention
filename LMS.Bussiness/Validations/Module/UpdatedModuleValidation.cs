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
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title can't be Empty")
       .NotNull().WithMessage("Title Is required");
            RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId can't be Empty")
           .NotNull().WithMessage("CourseId Is required");
            RuleFor(x => x.ModuleId).NotEmpty().WithMessage("ModuleId can't be Empty")
              .NotNull().WithMessage("ModuleId Is required");
        }

    }
}