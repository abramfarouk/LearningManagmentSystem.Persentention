using System.ComponentModel.DataAnnotations;

namespace LMS.Bussiness.DTOS.UserDto
{
    public class AddUserRequest
    {
        public string UserName => FirstName + "_" + LastName;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
