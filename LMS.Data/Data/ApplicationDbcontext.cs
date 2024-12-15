

using LMS.Data.Data.Entities;
using System.Reflection;

namespace LMS.Data.Data
{
    public class ApplicationDbcontext : IdentityDbContext<User, Role, int,
        IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>

    {

        #region Ctor 
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
        { }

        #endregion

        #region Tables 
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        }



    }
}
