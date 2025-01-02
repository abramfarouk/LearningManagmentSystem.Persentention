using LMS.Bussiness.DTOS.ForumPostDtos;
using LMS.Data.Abstract;
using LMS.Data.Bases;
using LMS.Data.Data.Entities;
using LMS.Data.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.Bussiness.Implementation
{
    public class ForumPostService : ResponseHandler, IForumPostService
    {
        private readonly IForumService _forumService;
        private readonly IGenericRepository<ForumPost> _forumRepo;
        private readonly UserManager<User> _userManager;
        public ForumPostService(IForumService forumService, IGenericRepository<ForumPost> forumRepo, UserManager<User> userManager)
        {
            _forumRepo = forumRepo;
            _forumService = forumService;
            _userManager = userManager;
        }
        public async Task<GResponse<string>> CreateForumPostAsync(AddForumPostRequest request)
        {
            try
            {
                var forum = _forumService.GetForumByIdAsync(request.ForumId);
                if (forum == null)
                {
                    return NotFound<string>("Forum not found");
                }
                var user = _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return NotFound<string>("User not found");
                }
                var forumPost = new ForumPost
                {
                    Content = request.Content,
                    ForumId = request.ForumId,
                    UserId = request.UserId,
                    PostDate = DateTime.UtcNow

                };
                var result = _forumRepo.AddAsync(forumPost);
                if (result == null)
                {
                    return BadRequest<string>("Failed to create forum post");
                }
                return Success<string>("Forum post created successfully");
            }
            catch (Exception e)
            {
                return BadRequest<string>($"Invalid an errors{e.Message}");
            }
        }
        public async Task<GResponse<string>> DeleteForumPostAsync(int id)
        {
            var forumPost = await _forumRepo.GetByIdAsync(id);
            if (forumPost == null)
            {
                return NotFound<string>("Forum post not found");
            }
            var result = await _forumRepo.DeleteAsync(forumPost);
            if (result == null)
            {
                return BadRequest<string>("Failed to delete forum post");
            }
            return Deleted<string>("Forum post deleted successfully");
        }

        public async Task<GResponse<List<ForumPostResponse>>> GetAllForumPostAsync()
        {
            var forumPosts = await _forumRepo.GetTableNoTracking().Include(x => x.User).Include(x => x.Forum).Select(x => new ForumPostResponse()
            {
                ForumPostId = x.Id,
                Content = x.Content,
                PostDate = new DateOnly(x.PostDate.Year, x.PostDate.Month, x.PostDate.Day),
                ForumName = x.Forum.Title,
                UserName = x.User.UserName,
            }).ToListAsync();
            if (forumPosts == null)
            {
                return NotFound<List<ForumPostResponse>>("No forum posts found");
            }
            return OK<List<ForumPostResponse>>(forumPosts, count: forumPosts.Count());
        }
        public async Task<GResponse<ForumPostResponse>> GetForumPostByIdAsync(int id)
        {
            var forumPost = _forumRepo.GetTableNoTracking().Include(x => x.User).Include(x => x.Forum).Select(x => new ForumPostResponse()
            {
                ForumPostId = x.Id,
                Content = x.Content,
                PostDate = new DateOnly(x.PostDate.Year, x.PostDate.Month, x.PostDate.Day),
                ForumName = x.Forum.Title,
                UserName = x.User.UserName,
            }).FirstOrDefault(x => x.ForumPostId == id);
            if (forumPost == null)
            {
                return NotFound<ForumPostResponse>("Forum post not found");
            }
            return OK<ForumPostResponse>(forumPost, count: 1);
        }
        public Task<GResponse<PigatedResult<ForumPostResponse>>> PaginatedListForumPostAsync(PaginatedListForumPostRequest request)
        {
            throw new NotImplementedException();
        }
        public async Task<GResponse<string>> UpdateForumPostAsync(UpdateForumPostRequest request)
        {
            var OldForumPost = await _forumRepo.GetByIdAsync(request.Id);
            if (OldForumPost == null)
            {
                return NotFound<string>("Forum post not found");
            }
            var user = _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return NotFound<string>("User not found");
            }
            var forum = _forumService.GetForumByIdAsync(request.ForumId);
            if (forum == null)
            {
                return NotFound<string>("Forum not found");
            }
            OldForumPost.PostDate = DateTime.UtcNow;
            OldForumPost.Content = request.Content;
            OldForumPost.UserId = request.UserId;
            OldForumPost.ForumId = request.ForumId;
            var result = await _forumRepo.UpdateAnsyc(OldForumPost);
            if (result == null)
            {
                return BadRequest<string>("Failed to update forum post");
            }
            return Success<string>("Forum post updated successfully");
        }
    }
}
