

namespace LMS.Data.Data.Entities.Identity
{
    public class User : IdentityUser<int>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
        public virtual ICollection<Course>? Courses { get; set; }
        public virtual ICollection<Certificate>? Certificates { get; set; }
        public virtual ICollection<ForumPost>? ForumPosts { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
        public virtual ICollection<Submission>? Submissions { get; set; }

        public virtual ICollection<UserRefreshToken>? UserRefreshTokens { get; set; }

    }

}
