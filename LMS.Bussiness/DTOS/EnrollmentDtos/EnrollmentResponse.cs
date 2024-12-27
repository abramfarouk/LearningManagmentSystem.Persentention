namespace LMS.Bussiness.DTOS.EnrollmentDtos
{
    public class EnrollmentResponse
    {
        public int Id { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public string CourseName { get; set; }
        public string UserName { get; set; }
    }
}
