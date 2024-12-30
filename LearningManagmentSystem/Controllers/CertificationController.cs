using LearningManagmentSystem.AppMetaData;
using LMS.Bussiness.DTOS.CertificateDtos;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagmentSystem.Controllers
{

    public class CertificationController : ControllerBase
    {
        private readonly ICertificationService _certificationService;
        public CertificationController(ICertificationService certificationService)
        {
            _certificationService = certificationService;
        }
        [HttpGet(Router.CertificationRouting.List)]
        public async Task<IActionResult> GetAllCertifications()
        {
            var result = await _certificationService.GetCerificationListAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet(Router.CertificationRouting.GetById)]
        public async Task<IActionResult> GetCertificationById(int Certificated_Id)
        {
            var Response = await _certificationService.GetCerificationByIdAsync(Certificated_Id);
            if (Response.IsSuccess)
            {
                return Ok(Response);
            }
            else
            {
                return BadRequest(Response);
            }

        }
        [HttpPost(Router.CertificationRouting.Create)]
        public async Task<IActionResult> AddCertification([FromBody] AddCeritificationRequest request)
        {
            var CertificationRespone = await _certificationService.AddCeritifcationAsync(request); ;
            if (CertificationRespone.IsSuccess)
                return Ok(CertificationRespone);
            return BadRequest(CertificationRespone);


        }
        [HttpDelete(Router.CertificationRouting.Delete)]
        public async Task<IActionResult> DeteleCertificationAnysc(int Certification_Id)
        {
            var response = await _certificationService.DeleteCeritifcationAsync(Certification_Id);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPut(Router.CertificationRouting.Edit)]
        public async Task<IActionResult> UpdateCertificationAsync(UpdateCeritificationRequest request)
        {
            var response = await _certificationService.UpdateCeritifcationAsync(request);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);

        }

    }
}
