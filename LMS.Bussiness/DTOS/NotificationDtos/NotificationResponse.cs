namespace LMS.Bussiness.DTOS.NotificationDtos
{
    public class NotificationResponse
    {
        public int NotificationId { get; set; }
        public string? Message { get; set; }
        public DateOnly SendDate { get; set; }
        public string UserName { get; set; }
    }
}
