using LMS.Bussiness.DTOS.FormsDtos;
using LMS.Data.Abstract;
using LMS.Data.Bases;
using LMS.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Bussiness.Implementation
{
    public class ForumService : ResponseHandler, IForumService
    {

        private readonly IGenericRepository<Forum> _forumRepo;
        private readonly ICourseService _courseService;
        public ForumService(IGenericRepository<Forum> forumRepo, ICourseService courseService)
        {
            _forumRepo = forumRepo;
            _courseService = courseService;
        }


        public async Task<GResponse<string>> CreateForumAsync(AddForumRequest request)
        {
            try
            {
                var course = await _courseService.GetCourseByIdAsync(request.CourseId);
                if (course == null)
                {
                    return NotFound<string>("Course not found");
                }
                var forum = new Forum
                {
                    Title = request.Title,
                    CourseId = request.CourseId
                };
                var result = await _forumRepo.AddAsync(forum);
                if (result == null)
                {
                    return BadRequest<string>("Failed to create forum");
                }
                return Success<string>("Forum created successfully");

            }
            catch (Exception e)
            {
                return BadRequest<string>($"Invalid an errors{e.Message}");
            }
        }

        public async Task<GResponse<string>> DeleteForumAsync(int id)
        {
            var forum = await _forumRepo.GetByIdAsync(id);
            if (forum == null)
            {
                return NotFound<string>("Forum not found");
            }
            var result = await _forumRepo.DeleteAsync(forum);
            if (result == null)
            {
                return BadRequest<string>("Failed to delete forum");
            }
            return Deleted<string>("Forum deleted successfully");
        }

        public async Task<GResponse<List<ForumResponse>>> GetAllForumsAsync()
        {
            var forums = await _forumRepo.GetTableNoTracking().Include(x => x.Course).Select(x => new ForumResponse()
            {
                Id = x.Id,
                Title = x.Title,
                CourseName = x.Course.Title,

            }).ToListAsync();
            if (forums == null)
            {
                return NotFound<List<ForumResponse>>("No forums found");
            }
            return OK<List<ForumResponse>>(forums, count: forums.Count());
        }

        public async Task<GResponse<ForumResponse>> GetForumByIdAsync(int id)
        {
            var forum = await _forumRepo.GetTableNoTracking().Include(x => x.Course).Select(x => new ForumResponse()
            {
                Id = x.Id,
                Title = x.Title,
                CourseName = x.Course.Title,
            }).FirstOrDefaultAsync(x => x.Id == id);
            if (forum == null)
            {
                return NotFound<ForumResponse>("Forum not found");
            }
            return OK<ForumResponse>(forum, count: 1);
        }

        public Task<GResponse<PigatedResult<ForumResponse>>> PaginatedListForumAsync(PaginatedListForumRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GResponse<string>> UpdateForumAsync(UpdateForumRequest request)
        {
            var OldForum = await _forumRepo.GetTableNoTracking().Include(x => x.Course).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (OldForum == null)
            {
                return NotFound<string>("Forum not found");
            }
            var course = await _courseService.GetCourseByIdAsync(request.CourseId);
            if (course == null)
            {
                return NotFound<string>("Course not found");
            }
            OldForum.Title = request.Title;
            OldForum.CourseId = request.CourseId;
            var result = await _forumRepo.UpdateAnsyc(OldForum);
            if (result == null)
            {
                return BadRequest<string>("Failed to update forum");
            }
            return Success<string>("Forum updated successfully");

        }
    }
}
