﻿namespace LMS.Bussiness.DTOS.UserDto
{
    public class UpdateUserRequest
    {
        public int UserId { get; set; }
        public string UserName => FirstName + "_" + LastName;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }


    }
}
