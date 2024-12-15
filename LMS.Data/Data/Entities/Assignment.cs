

namespace LMS.Data.Data.Entities
{
    public class Assignment : IBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public int? CourseId { get; set; }
        public virtual Course? Course { get; set; }
        public virtual ICollection<Submission>? Submissions { get; set; } = new HashSet<Submission>();
    }
}
