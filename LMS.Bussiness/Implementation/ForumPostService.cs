using LMS.Bussiness.DTOS.ForumPostDtos;
using LMS.Data.Bases;

namespace LMS.Bussiness.Implementation
{
    public class ForumPostService : ResponseHandler, IForumPostService
    {
        public Task<GResponse<string>> CreateForumPostAsync(AddForumPostRequest request)
        {
            throw new NotImplementedException();
        }
        public Task<GResponse<string>> DeleteForumPostAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<List<ForumPostResponse>>> GetAllForumPostAsync()
        {
            throw new NotImplementedException();
        }
        public Task<GResponse<ForumPostResponse>> GetForumPostByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<GResponse<PigatedResult<ForumPostResponse>>> PaginatedListForumPostAsync(PaginatedListForumPostRequest request)
        {
            throw new NotImplementedException();
        }
        public Task<GResponse<string>> UpdateForumPostAsync(UpdateForumPostRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
