namespace LMS.Data.Data.Entities
{
    public class Forum : IBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public int? CourseId { get; set; }
        public Course? Course { get; set; } = null!;

        public ICollection<ForumPost>? ForumPosts { get; set; } = new HashSet<ForumPost>();
    }
}
