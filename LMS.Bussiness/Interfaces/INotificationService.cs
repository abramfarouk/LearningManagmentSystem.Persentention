using LMS.Bussiness.DTOS.NotificationDtos;
using LMS.Data.Bases;

namespace LMS.Bussiness.Interfaces
{
    public interface INotificationService
    {

        public Task<GResponse<string>> AddNotificationAsync(AddNotificationRequest request);
        public Task<GResponse<string>> DeleteNotificationAsync(int notificationId);
        public Task<GResponse<string>> UpdateNotificationAsync(UpdateNotificationRequest request);
        public Task<GResponse<List<NotificationResponse>>> GetAllNotificationListAsync();
        public Task<GResponse<NotificationResponse>> GetNotificationByIdAsync(int notificationId);
        public Task<PigatedResult<NotificationResponse>> NotificationPaginatedListAsync(NotificationPaginatedListRequest request);

    }
}
