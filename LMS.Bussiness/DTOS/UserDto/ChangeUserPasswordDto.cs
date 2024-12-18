namespace LMS.Bussiness.DTOS.UserDto
{
    public class ChangeUserPasswordDto
    {
        public int Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
