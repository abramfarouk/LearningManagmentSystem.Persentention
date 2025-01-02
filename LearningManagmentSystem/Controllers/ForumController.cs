using LearningManagmentSystem.AppMetaData;
using LMS.Bussiness.DTOS.FormsDtos;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly IForumService _forumService;

        public ForumController(IForumService forumService)
        {
            _forumService = forumService;
        }
        [HttpGet(Router.ForumRouting.List)]
        public async Task<IActionResult> GetForumListAsync()
        {
            var response = await _forumService.GetAllForumsAsync();
            if (response == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet(Router.ForumRouting.GetById)]
        public async Task<IActionResult> GetForumByIdAsync(int id)
        {
            var response = await _forumService.GetForumByIdAsync(id);
            if (response == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost(Router.ForumRouting.Create)]

        public async Task<IActionResult> CreateForumAsync([FromBody] AddForumRequest request)
        {
            var response = await _forumService.CreateForumAsync(request);
            if (response == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete(Router.ForumRouting.Delete)]
        public async Task<IActionResult> DeleteForumAsync(int id)
        {
            var response = await _forumService.DeleteForumAsync(id);
            if (response == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut(Router.ForumRouting.Edit)]
        public async Task<IActionResult> EditForumAsync([FromBody] UpdateForumRequest request)
        {
            var response = await _forumService.UpdateForumAsync(request);
            if (response == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
