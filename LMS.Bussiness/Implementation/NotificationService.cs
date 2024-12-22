using LMS.Bussiness.DTOS.NotificationDtos;
using LMS.Data.Abstract;
using LMS.Data.Bases;
using LMS.Data.Data.Entities;
using LMS.Data.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.Bussiness.Implementation
{
    public class NotificationService : ResponseHandler, INotificationService
    {
        private readonly IGenericRepository<Notification> _notificationRepo;
        private readonly UserManager<User> _userManager;
        public NotificationService(IGenericRepository<Notification> notificationRepository, UserManager<User> userManager)
        {
            _notificationRepo = notificationRepository;
            _userManager = userManager;
        }
        public async Task<GResponse<string>> AddNotificationAsync(AddNotificationRequest request)
        {
            try
            {
                var users = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (users == null)
                {
                    return NotFound<string>($"the user Id {request.UserId} not found ");
                }
                var notification = new Notification
                {
                    Message = request.Message,
                    SendDate = DateTime.UtcNow,
                    UserId = request.UserId
                };
                var result = await _notificationRepo.AddAsync(notification);
                if (result)
                {
                    return Success<string>("the Notification Is Successfull !");
                }
                else
                {
                    return BadRequest<string>("the Notification Is Failure ");
                }
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invilad an errors {ex.Message}");
            }
        }

        public async Task<GResponse<string>> DeleteNotificationAsync(int notificationId)
        {
            try
            {
                var notification = await _notificationRepo.GetByIdAsync(notificationId);
                if (notification == null)
                {
                    return NotFound<string>($"the notification Id {notificationId} not found ");
                }
                var result = await _notificationRepo.DeleteAsync(notification);
                if (result)
                {
                    return Deleted<string>("the Notification Is Deleted !");
                }
                else
                {
                    return BadRequest<string>("the Notification Is Failure ");
                }
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Invilad an errors {ex.Message}");
            }
        }

        public async Task<GResponse<List<NotificationResponse>>> GetAllNotificationListAsync()
        {
            try
            {
                var notifications = await _notificationRepo.GetTableNoTracking().Include(x => x.User).Select(notificated => new NotificationResponse()
                {
                    NotificationId = notificated.Id,
                    Message = notificated.Message,
                    SendDate = new DateOnly(notificated.SendDate.Year, notificated.SendDate.Month, notificated.SendDate.Day),
                    UserName = notificated.User.UserName ?? "No User Name"
                }).ToListAsync();
                if (!notifications.Any())
                {
                    return NotFound<List<NotificationResponse>>("No Notification Found");
                }
                else
                {
                    return OK<List<NotificationResponse>>(notifications, count: notifications.Count());
                }
            }
            catch (Exception ex)
            {
                return BadRequest<List<NotificationResponse>>($"Invalid an errors {ex.Message}");
            }
        }

        public async Task<GResponse<NotificationResponse>> GetNotificationByIdAsync(int notificationId)
        {
            try
            {
                var notification = await _notificationRepo.GetTableNoTracking().Include(x => x.User).Select(notificated => new NotificationResponse()
                {
                    NotificationId = notificated.Id,
                    Message = notificated.Message,
                    SendDate = new DateOnly(notificated.SendDate.Year, notificated.SendDate.Month, notificated.SendDate.Day),
                    UserName = notificated.User.UserName ?? "No User Name"
                }).FirstOrDefaultAsync(x => x.NotificationId == notificationId);
                if (notification == null)
                {
                    return NotFound<NotificationResponse>("No Notification Found");
                }
                else
                {
                    return OK(notification, count: 1);
                }
            }
            catch (Exception ex)
            {
                return BadRequest<NotificationResponse>($"Invaild an errors {ex.Message}");
            }
        }

        public async Task<PigatedResult<NotificationResponse>> NotificationPaginatedListAsync(NotificationPaginatedListRequest request)
        {
            var NotificationQuery = _notificationRepo.GetTableNoTracking().Include(x => x.User).Select(x => new NotificationResponse()
            {
                NotificationId = x.Id,
                UserName = x.User.UserName,
                Message = x.Message,
                SendDate = new DateOnly(x.SendDate.Year, x.SendDate.Month, x.SendDate.Day)
            }).AsQueryable();
            if (!NotificationQuery.Any())
            {
                return new PigatedResult<NotificationResponse>(new List<NotificationResponse>());

            }
            var result = await NotificationQuery.ToPaginatedListAsync(request.NumberPage, request.PageSize);
            return result;

        }

        public async Task<GResponse<string>> UpdateNotificationAsync(UpdateNotificationRequest request)
        {
            try
            {
                var notification = await _notificationRepo.GetByIdAsync(request.NotificationId);
                if (notification == null)
                {
                    return NotFound<string>($"the notification Id {request.NotificationId} not found ");
                }
                var users = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (users == null)
                {
                    return NotFound<string>($"the user Id {request.UserId} not found ");
                }
                notification.Message = request.Message;
                notification.SendDate = DateTime.UtcNow;
                notification.UserId = request.UserId;

                var result = await _notificationRepo.UpdateAnsyc(notification);
                if (result)
                {
                    return Success<string>("the Notification Is Updated !");
                }
                else
                {
                    return BadRequest<string>("the Notification Is Failure ");
                }
            }

            catch (Exception ex)
            {
                return BadRequest<string>($"Invaild an errors {ex.Message}");
            }

        }
    }
}
