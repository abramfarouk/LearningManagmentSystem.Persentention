using LearningManagmentSystem.AppMetaData;
using LMS.Bussiness.DTOS.NotificationDtos;
using LMS.Bussiness.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LearningManagmentSystem.Controllers
{

    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet(Router.NotificationRouting.List)]
        public async Task<IActionResult> GetNotificationListAsync()
        {
            var response = await _notificationService.GetAllNotificationListAsync();
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpGet(Router.NotificationRouting.GetById)]
        public async Task<IActionResult> GetNotificationByIdAsync(int NotificationId)
        {
            var response = await _notificationService.GetNotificationByIdAsync(NotificationId);
            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500, response);

            if (!response.IsSuccess && response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet(Router.NotificationRouting.Paginated)]
        public async Task<IActionResult> GetNotificationPaginatedListAsync([FromQuery] NotificationPaginatedListRequest request)
        {
            var response = await _notificationService.NotificationPaginatedListAsync(request);
            if (response.Successed == false)
                return StatusCode(500, "Internal Error.");

            return Ok(response);
        }

        [HttpPost(Router.NotificationRouting.Create)]
        public async Task<IActionResult> CreateNotificationAsync([FromBody] AddNotificationRequest request)
        {
            var response = await _notificationService.AddNotificationAsync(request);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);

        }
        [HttpPut(Router.NotificationRouting.Edit)]
        public async Task<IActionResult> UpdateNotificationAsync(UpdateNotificationRequest request)
        {
            var response = await _notificationService.UpdateNotificationAsync(request);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete(Router.NotificationRouting.Delete)]
        public async Task<IActionResult> DeleteNotificationAsync(int NotificationId)
        {
            var response = await _notificationService.DeleteNotificationAsync(NotificationId);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
