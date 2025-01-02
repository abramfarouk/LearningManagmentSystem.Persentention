using LMS.Bussiness.DTOS.ForumPostDtos;
using LMS.Data.Bases;

namespace LMS.Bussiness.Interfaces
{
    public interface IForumPostService
    {
        public Task<GResponse<string>> CreateForumPostAsync(AddForumPostRequest request);
        public Task<GResponse<string>> UpdateForumPostAsync(UpdateForumPostRequest request);
        public Task<GResponse<string>> DeleteForumPostAsync(int id);
        public Task<GResponse<ForumPostResponse>> GetForumPostByIdAsync(int id);
        public Task<GResponse<List<ForumPostResponse>>> GetAllForumPostAsync();
        public Task<GResponse<PigatedResult<ForumPostResponse>>> PaginatedListForumPostAsync(PaginatedListForumPostRequest request);
    }
}
