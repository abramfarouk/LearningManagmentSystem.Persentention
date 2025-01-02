using LMS.Bussiness.DTOS.FormsDtos;
using LMS.Data.Bases;

namespace LMS.Bussiness.Implementation
{
    public class ForumService : ResponseHandler, IForumService
    {
        public Task<GResponse<string>> CreateForumAsync(AddForumRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<string>> DeleteForumAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<List<ForumResponse>>> GetAllForumsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<ForumResponse>> GetForumByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<PigatedResult<ForumResponse>>> PaginatedListForumAsync(PaginatedListForumRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<string>> UpdateForumAsync(UpdateForumRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
