using LMS.Bussiness.DTOS.NotificationDtos;
using LMS.Data.Abstract;
using LMS.Data.Bases;
using LMS.Data.Data.Entities;

namespace LMS.Bussiness.Implementation
{
    public class NotificationService : ResponseHandler, INotificationService
    {
        private readonly IGenericRepository<Notification> _notificationRepo;
        public NotificationService(IGenericRepository<Notification> notificationRepository)
        {
            _notificationRepo = notificationRepository;
        }

        public Task<GResponse<string>> AddNotificationAsync(AddNotificationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<string>> DeleteNotificationAsync(int notificationId)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<List<NotificationResponse>>> GetAllNotificationListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<NotificationResponse>> GetNotificationByIdAsync(int notificationId)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<PigatedResult<NotificationResponse>>> NotificationPaginatedListAsync(NotificationPaginatedListRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GResponse<string>> UpdateNotificationAsync(UpdateNotificationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
