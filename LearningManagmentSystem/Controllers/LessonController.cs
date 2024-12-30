using LMS.Bussiness.DTOS.LessonDtos;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {

        private readonly ILessonService _lessonService;
        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }
        [HttpGet("GetLessons")]
        public async Task<IActionResult> GetLessonsAsync()
        {
            var response = await _lessonService.GetLessonsAsync();
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("GetLessonById/{lessonId}")]
        public async Task<IActionResult> GetLessonByIdAsync(int lessonId)
        {
            var response = await _lessonService.GetLessonByIdAsync(lessonId);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPut("UpdateLesson")]
        public async Task<IActionResult> UpdateLessonAsync([FromForm] UpdateLessonRequest request)
        {
            var response = await _lessonService.UpdateLessonAsync(request);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPost("AddLesson")]
        public async Task<IActionResult> AddLessonAsync([FromForm] AddLessonRequest request)
        {
            var response = await _lessonService.AddLessonAsync(request);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete("DeleteLesson/{lessonId}")]
        public async Task<IActionResult> DeleteLessonAsync(int lessonId)
        {
            var response = await _lessonService.DeleteLessonAsync(lessonId);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet("PaginatedList")]
        public async Task<IActionResult> GetLessonsPaginatedListAsync([FromQuery] LessonPaginatedListRequest request)
        {
            var response = await _lessonService.GetLessonsPaginatedListAsync(request);
            if (response.Successed)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
