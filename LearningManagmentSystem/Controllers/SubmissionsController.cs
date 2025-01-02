using LearningManagmentSystem.AppMetaData;
using LMS.Bussiness.DTOS.SubmissionDtos;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LearningManagmentSystem.Controllers
{

    public class SubmissionsController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;
        public SubmissionsController(ISubmissionService submissionService)
        {
            _submissionService = submissionService;
        }
        [HttpPost(Router.SubmissionRouting.Create)]
        public async Task<IActionResult> CreateSubmissionAsync([FromBody] AddSubmissionRequest request)
        {
            var response = await _submissionService.CreateSubmissionAsync(request);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);
            return Ok(response);
        }
        [HttpGet(Router.SubmissionRouting.List)]
        public async Task<IActionResult> GetAllSubmissionsAsync()
        {
            var response = await _submissionService.GetAllSubmissionsAsync();
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);
            return Ok(response);
        }
        [HttpGet(Router.SubmissionRouting.GetById)]
        public async Task<IActionResult> GetSubmissionByIdAsync(int id)
        {
            var response = await _submissionService.GetSubmissionByIdAsync(id);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);
            return Ok(response);
        }
        [HttpPut(Router.SubmissionRouting.Edit)]
        public async Task<IActionResult> UpdateSubmissionAsync([FromBody] UpdateSubmissionRequest request)
        {
            var response = await _submissionService.UpdateSubmissionAsync(request);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);
            return Ok(response);
        }
        [HttpDelete(Router.SubmissionRouting.Delete)]
        public async Task<IActionResult> DeleteSubmissionAsync(int id)
        {
            var response = await _submissionService.DeleteSubmissionAsync(id);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.NotFound)
                return NotFound(response);
            return Ok(response);

        }
    }
}
