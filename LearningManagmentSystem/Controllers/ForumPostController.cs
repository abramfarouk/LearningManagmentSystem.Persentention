using LearningManagmentSystem.AppMetaData;
using LMS.Bussiness.DTOS.ForumPostDtos;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagmentSystem.Controllers
{

    public class ForumPostController : ControllerBase
    {

        private readonly IForumPostService _forumPostService;
        public ForumPostController(IForumPostService forumPostService)
        {
            _forumPostService = forumPostService;
        }
        [HttpGet(Router.ForumPostRouting.List)]
        public async Task<IActionResult> GetForumPostListAsync()
        {
            var response = await _forumPostService.GetAllForumPostAsync();
            if (response == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet(Router.ForumPostRouting.GetById)]
        public async Task<IActionResult> GetForumPostByIdAsync(int id)
        {
            var response = await _forumPostService.GetForumPostByIdAsync(id);
            if (response == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost(Router.ForumPostRouting.Create)]
        public async Task<IActionResult> CreateForumPostAsync([FromBody] AddForumPostRequest request)
        {
            var response = await _forumPostService.CreateForumPostAsync(request);
            if (response == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete(Router.ForumPostRouting.Delete)]
        public async Task<IActionResult> DeleteForumPostAsync(int id)
        {
            var response = await _forumPostService.DeleteForumPostAsync(id);
            if (response == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut(Router.ForumPostRouting.Edit)]
        public async Task<IActionResult> UpdateForumPostAsync([FromBody] UpdateForumPostRequest request)
        {
            var response = await _forumPostService.UpdateForumPostAsync(request);
            if (response == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
