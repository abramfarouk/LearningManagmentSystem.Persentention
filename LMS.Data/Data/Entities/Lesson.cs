namespace LMS.Data.Data.Entities
{
    public class Lesson : IBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public string? UrlVedio { get; set; }
        public int ModuleId { get; set; }
        public virtual Module? Module { get; set; }

    }
}