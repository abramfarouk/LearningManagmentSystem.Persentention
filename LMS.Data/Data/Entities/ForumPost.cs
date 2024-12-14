namespace LMS.Data.Data.Entities
{
    public class ForumPost : IBaseEntity
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime PostDate { get; set; }

        public int ForumId { get; set; }
        public Forum Forum { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}