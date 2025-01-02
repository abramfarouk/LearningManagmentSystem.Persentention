using LMS.Bussiness.DTOS.FormsDtos;
using LMS.Data.Bases;

namespace LMS.Bussiness.Interfaces
{
    public interface IForumService
    {
        public Task<GResponse<string>> CreateForumAsync(AddForumRequest request);
        public Task<GResponse<string>> UpdateForumAsync(UpdateForumRequest request);
        public Task<GResponse<string>> DeleteForumAsync(int id);
        public Task<GResponse<ForumResponse>> GetForumByIdAsync(int id);
        public Task<GResponse<List<ForumResponse>>> GetAllForumsAsync();
        public Task<GResponse<PigatedResult<ForumResponse>>> PaginatedListForumAsync(PaginatedListForumRequest request);
    }
}
