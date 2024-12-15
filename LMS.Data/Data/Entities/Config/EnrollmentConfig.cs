using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Data.Data.Entities.Config
{
    internal class EnrollmentConfig : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("Enrollments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.EnrollmentDate).IsRequired();
            builder.HasOne(x => x.Course).WithMany(c => c.Enrollments).HasForeignKey(x => x.CourseId);
            builder.HasOne(x => x.User).WithMany(u => u.Enrollments).HasForeignKey(x => x.UserId);
        }
    }
}
