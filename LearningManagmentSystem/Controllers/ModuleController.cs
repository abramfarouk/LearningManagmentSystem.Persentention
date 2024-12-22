using LMS.Bussiness.DTOS.ModuleDtos;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _moduleService;
        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }
        [HttpGet("List")]
        public async Task<IActionResult> GetModuleAsync()
        {
            var response = await _moduleService.GetAllModuleListAsync();
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetModuleByIdAsync(int moduleId)
        {
            var response = await _moduleService.GetModuleByIdAsync(moduleId);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }


        [HttpPost]
        public async Task<IActionResult> AddModuleAsync(AddModuleRequest request)
        {
            var response = await _moduleService.AddModuleAsync(request);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("PaginatedList")]
        public async Task<IActionResult> PaginatedModuleListAsync([FromQuery] ModulePaginatedListRequest request)
        {
            var response = await _moduleService.PaginatedModuleListAsync(request);
            if (response.Successed)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatedModuleAsync(UpdatedModuleRequest request)
        {
            var response = await _moduleService.UpdatedModuleAsync(request);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteModuleAsync(int moduleId)
        {
            var response = await _moduleService.DeleteModuleAsync(moduleId);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
