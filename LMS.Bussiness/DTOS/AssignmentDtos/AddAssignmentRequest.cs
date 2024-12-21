namespace LMS.Bussiness.DTOS.AssignmentDtos
{
    public class AddAssignmentRequest
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int CourseId { get; set; }
    }
}
