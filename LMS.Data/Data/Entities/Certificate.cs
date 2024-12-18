

namespace LMS.Data.Data.Entities
{
    public class Certificate : IBaseEntity
    {
        public int Id { get; set; }

        public DateTime IssueDate { get; set; }
        public int? CourseId { get; set; }
        public Course? Course { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }

    }
}
