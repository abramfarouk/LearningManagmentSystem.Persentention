using LMS.Bussiness.DTOS.AssignmentDtos;

namespace LMS.Bussiness.Implementation
{
    public class AssignmentService : IAssignmentService
    {
        public Task<GResponse<string>> AddAssignmentAsync(AddAssignmentRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<string>> DeleteAssignmentAsync(int Assignment_Id)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<IEnumerable<GetAssignmentResponseDto>>> GetAllAssignmentListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<GetAssignmentResponseDto>> GetAssignmentByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<GetAssignmentResponseDto>> GetPaginatedAssignmentListAsync(GetAssignmentPaginatedListRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<string>> UpdatedAssignmentAsync(UpdatedAssignmentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
