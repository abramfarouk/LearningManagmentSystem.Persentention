
namespace LMS.Data.Data.Entities
{
    public class Notification : IBaseEntity
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public DateTime SendDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
