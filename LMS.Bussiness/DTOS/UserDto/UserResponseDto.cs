namespace LMS.Bussiness.DTOS.UserDto
{
    public class UserResponseDto : UpdateUserRequest
    {

        public List<string>? Roles { get; set; }
    }
}