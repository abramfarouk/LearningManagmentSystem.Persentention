using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Data.Data.Entities.Config
{
    public class AssignmentConfig : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.ToTable("Assignmets");
            builder.Property(x => x.Title).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.HasOne(s => s.Course).WithMany(c => c.Assignments).HasForeignKey(s => s.CourseId);

        }
    }
}
