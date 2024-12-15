namespace LMS.Data.Data.Entities
{
    public class Grade : IBaseEntity
    {
        public int Id { get; set; }
        public float grade { get; set; }

        public int? SubmissionId { get; set; }
        public Submission? Submission { get; set; } = null!;

    }
}