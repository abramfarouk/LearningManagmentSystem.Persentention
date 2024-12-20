namespace LMS.Bussiness.DTOS.CertificateDtos
{
    public class CerificationResponseDto
    {
        public int CeritifedId { get; set; }
        public DateOnly IssueDate { get; set; }
        public string CourseName { get; set; } = null!;
        public string StudentName { get; set; } = null!;
    }
}
