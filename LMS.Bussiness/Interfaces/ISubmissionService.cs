using LMS.Bussiness.DTOS.SubmissionDtos;
using LMS.Data.Bases;

namespace LMS.Bussiness.Interfaces
{
    public interface ISubmissionService
    {
        public Task<GResponse<string>> CreateSubmissionAsync(AddSubmissionRequest request);
        public Task<GResponse<string>> UpdateSubmissionAsync(UpdateSubmissionRequest request);
        public Task<GResponse<string>> DeleteSubmissionAsync(int id);
        public Task<GResponse<SubmissionResponseDto>> GetSubmissionByIdAsync(int id);
        public Task<GResponse<List<SubmissionResponseDto>>> GetAllSubmissionsAsync();
        public Task<GResponse<PigatedResult<SubmissionResponseDto>>> PaginatedSubmissionListAsync(PaginatedListSubmissionRequest pagination);
    }
}
