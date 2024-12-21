namespace LMS.Bussiness.DTOS.AssignmentDtos
{
    public class GetAssignmentResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateOnly CreatedDate { get; set; }

        public string CourseName { get; set; }
    }
}
