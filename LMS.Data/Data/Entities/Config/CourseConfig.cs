using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Data.Data.Entities.Config
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.Property(c => c.Title).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired();
            builder.Property(c => c.Level).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired(false);
            builder.Property(c => c.CreateDate).IsRequired();
            builder.HasOne(c => c.User).WithMany(u => u.Courses).HasForeignKey(c => c.UserId);


        }
    }
}
