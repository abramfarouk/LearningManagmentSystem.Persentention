namespace LMS.Bussiness.DTOS.CourseDtos
{
    public class AddCourseRequest
    {
        public string Title { get; set; }
        public string? Level { get; set; }
        public string? Description { get; set; }
        public int TeacherId { get; set; }
    }
}
