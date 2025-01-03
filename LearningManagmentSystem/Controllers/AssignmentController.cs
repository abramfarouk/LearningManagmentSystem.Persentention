using LMS.Bussiness.DTOS.AssignmentDtos;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Router = LearningManagmentSystem.AppMetaData.Router;

namespace LearningManagmentSystem.Controllers
{

    [Authorize]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;
        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpGet(Router.AssignmentRouting.Paginated)]
        public async Task<IActionResult> GetAssignmentPaginatedListAsync([FromQuery] GetAssignmentPaginatedListRequest request)
        {
            var response = await _assignmentService.GetPaginatedAssignmentListAsync(request);
            if (response.Successed == false)
                return StatusCode(500, "Internal Error.");

            return Ok(response);

        }

        [HttpPost]
        public async Task<IActionResult> CreateAssignmentAsync([FromBody] AddAssignmentRequest request)
        {
            var response = await _assignmentService.AddAssignmentAsync(request);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);

        }

        [HttpGet(Router.AssignmentRouting.List)]
        public async Task<IActionResult> GetAssignmentAsync()
        {
            var response = await _assignmentService.GetAllAssignmentListAsync();
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet(Router.AssignmentRouting.GetById)]
        public async Task<IActionResult> GetAssignmentByIdAsync(int AssignmentId)
        {
            var response = await _assignmentService.GetAssignmentByIdAsync(AssignmentId);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);

        }

        [HttpPut(Router.AssignmentRouting.Edit)]
        public async Task<IActionResult> UpdatedAssignmentAsync(UpdatedAssignmentRequest request)
        {
            var response = await _assignmentService.UpdatedAssignmentAsync(request);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete(Router.AssignmentRouting.Delete)]
        public async Task<IActionResult> RemoveAssignmentAsync(int Id)
        {
            var response = await _assignmentService.DeleteAssignmentAsync(Id);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

    }
}
