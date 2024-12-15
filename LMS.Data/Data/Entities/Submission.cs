namespace LMS.Data.Data.Entities
{
    public class Submission : IBaseEntity
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime SubmissionDate { get; set; }

        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; } = null!;
        public Grade grade { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
