

namespace LMS.Data.Data.Entities
{
    public class Course : IBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } //Required
        public string? Level { get; set; } //Optional
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual ICollection<Module>? Modules { get; set; } = new HashSet<Module>();
        public virtual ICollection<Forum>? Forums { get; set; } = new HashSet<Forum>();
        public virtual ICollection<Certificate>? Certificates { get; set; } = new HashSet<Certificate>();
        public virtual ICollection<Assignment>? Assignments { get; set; } = new HashSet<Assignment>();
        public virtual ICollection<Enrollment>? Enrollments { get; set; } = new HashSet<Enrollment>();

        public int? UserId { get; set; }
        public virtual User? User { get; set; } = null!; //Nevigation Property
    }
}
