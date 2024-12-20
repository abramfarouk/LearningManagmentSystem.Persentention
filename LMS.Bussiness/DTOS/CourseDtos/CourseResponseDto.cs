namespace LMS.Bussiness.DTOS.CourseDtos
{
    public class CourseResponseDto
    {
        public int Cr_Id { get; set; }
        public string Title { get; set; }
        public string? Level { get; set; }
        public string? Description { get; set; }
        public DateOnly CreatedTime { get; set; }
        public string TeacherName { get; set; }
    }
}
