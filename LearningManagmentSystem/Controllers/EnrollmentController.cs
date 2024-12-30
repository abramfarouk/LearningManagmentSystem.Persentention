using LearningManagmentSystem.AppMetaData;
using LMS.Bussiness.DTOS.EnrollmentDtos;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagmentSystem.Controllers
{

    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet(Router.EnrollmentRouting.List)]
        public async Task<IActionResult> GetEnrollments()
        {
            var response = await _enrollmentService.GetEnrollmentsAsync();
            if (response.IsSuccess)

                return Ok(response);
            return BadRequest(response);

        }

        [HttpGet(Router.EnrollmentRouting.GetById)]
        public async Task<IActionResult> GetEnrollmentById(int id)
        {
            var response = await _enrollmentService.GetEnrollmentByIdAsync(id);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("EnrollmentListPaginated")]
        public async Task<IActionResult> EnrollmentListPaginated([FromQuery] EnrollmentPaginatedListRequest request)
        {
            var response = await _enrollmentService.EnrollmentListPaginatedAsync(request);
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPost(Router.EnrollmentRouting.Create)]
        public async Task<IActionResult> CreateEnrollment(AddEnrollmentRequest request)
        {
            var response = await _enrollmentService.CreateEnrollmentAsync(request);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPut(Router.EnrollmentRouting.Edit)]
        public async Task<IActionResult> UpdateEnrollment(UpdateEnrollmentRequest request)
        {
            var response = await _enrollmentService.UpdateEnrollmentAsync(request);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpDelete(Router.EnrollmentRouting.Delete)]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var response = await _enrollmentService.DeleteEnrollmentAsync(id);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

    }
}
