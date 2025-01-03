using LearningManagmentSystem.AppMetaData;
using LMS.Bussiness.DTOS.GradeDtos;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagmentSystem.Controllers
{

    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }
        [HttpPost(Router.GradeRouting.Create)]
        public async Task<IActionResult> CreateGradeAsync(AddGradeRequest request)
        {
            var response = await _gradeService.CreateGradeAsync(request);
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpDelete(Router.GradeRouting.Delete)]
        public async Task<IActionResult> DeleteGradeAsync(int gradeId)
        {
            var response = await _gradeService.DeleteGradeAsync(gradeId);
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet(Router.GradeRouting.List)]
        public async Task<IActionResult> GetGradesAsync()
        {
            var response = await _gradeService.GetAllGradesAsync();
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet(Router.GradeRouting.GetById)]
        public async Task<IActionResult> GetGradeByIdAsync(int gradeId)
        {
            var response = await _gradeService.GetGradesByIdAsync(gradeId);
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPut(Router.GradeRouting.Edit)]
        public async Task<IActionResult> UpdateGradeAsync(UpdateGradeRequest request)
        {
            var response = await _gradeService.UpdateGradeAsync(request);
            if (response != null)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
