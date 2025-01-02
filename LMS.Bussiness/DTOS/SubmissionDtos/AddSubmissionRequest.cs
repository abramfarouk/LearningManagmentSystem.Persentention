namespace LMS.Bussiness.DTOS.SubmissionDtos
{
    public class AddSubmissionRequest
    {
        public string Content { get; set; }
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
    }
}
