using LearningManagmentSystem.AppMetaData;
using LMS.Bussiness.DTOS.CourseDtos;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagmentSystem.Controllers
{

    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet(Router.CourseRouting.List)]
        public async Task<IActionResult> GetAllCourses()
        {
            var result = await _courseService.GetAllCoursesAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet(Router.CourseRouting.GetById)]
        public async Task<IActionResult> GetCourseById(int courseId)
        {
            var result = await _courseService.GetCourseByIdAsync(courseId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost(Router.CourseRouting.Create)]
        public async Task<IActionResult> AddCourse([FromBody] AddCourseRequest request)
        {
            var result = await _courseService.AddCourseAsync(request);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete(Router.CourseRouting.Delete)]
        public async Task<IActionResult> RemoveCourse(int Crs_Id)
        {
            var result = await _courseService.DeleteCourseAsync(Crs_Id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut(Router.CourseRouting.Edit)]
        public async Task<IActionResult> UpdateCourse(UpdateCourseRequest request)
        {
            var res = await _courseService.UpdateCourseAsync(request);
            if (res.IsSuccess)

                return Ok(res);

            return BadRequest(res);
        }

    }
}
