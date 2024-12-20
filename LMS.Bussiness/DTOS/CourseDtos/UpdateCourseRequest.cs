namespace LMS.Bussiness.DTOS.CourseDtos
{
    public class UpdateCourseRequest
    {
        public int Crs_Id { get; set; }
        public string Title { get; set; }
        public string? Level { get; set; }
        public string? Description { get; set; }

    }
}
