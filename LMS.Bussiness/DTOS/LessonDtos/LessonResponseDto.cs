namespace LMS.Bussiness.DTOS.LessonDtos
{
    public class LessonResponseDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? UrlVedio { get; set; }

        public string? ModuleName { get; set; }
    }
}
