namespace LMS.Bussiness.DTOS.ModuleDtos
{
    public class GetModuleResponseDto
    {
        public int ModuleId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string CourseName { get; set; }
    }
}
