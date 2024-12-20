using LMS.Bussiness.DTOS.CourseDtos;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet("Get-All-Courses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var result = await _courseService.GetAllCoursesAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("Get-Course-By-Id/{courseId}")]
        public async Task<IActionResult> GetCourseById(int courseId)
        {
            var result = await _courseService.GetCourseByIdAsync(courseId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Add-Course")]
        public async Task<IActionResult> AddCourse([FromBody] AddCourseRequest request)
        {
            var result = await _courseService.AddCourseAsync(request);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("Delete-Course/{Crs_Id}")]
        public async Task<IActionResult> RemoveCourse(int Crs_Id)
        {
            var result = await _courseService.DeleteCourseAsync(Crs_Id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("Update_Course")]
        public async Task<IActionResult> UpdateCourse(UpdateCourseRequest request)
        {
            var res = await _courseService.UpdateCourseAsync(request);
            if (res.IsSuccess)

                return Ok(res);

            return BadRequest(res);
        }

    }
}
