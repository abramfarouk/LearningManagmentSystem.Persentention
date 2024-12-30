using Microsoft.AspNetCore.Http;

namespace LMS.Bussiness.DTOS.LessonDtos
{
    public class AddLessonRequest
    {
        public string Title { get; set; }
        public string? Content { get; set; }
        public IFormFile? VedioFile { get; set; }

        public int ModuleId { get; set; }
    }
}
