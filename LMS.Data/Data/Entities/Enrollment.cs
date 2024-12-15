namespace LMS.Data.Data.Entities
{
    public class Enrollment : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
