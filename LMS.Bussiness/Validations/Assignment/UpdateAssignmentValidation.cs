using FluentValidation;
using LMS.Bussiness.DTOS.AssignmentDtos;

namespace LMS.Bussiness.Validations.Assignment
{
    public class UpdateAssignmentValidation : AbstractValidator<UpdatedAssignmentRequest>
    {
        public UpdateAssignmentValidation()
        {
            ApplyVaalidationRules();
        }

        public void ApplyVaalidationRules()
        {

        }
    }
}
