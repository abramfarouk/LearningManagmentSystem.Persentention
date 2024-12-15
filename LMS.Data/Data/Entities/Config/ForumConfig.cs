using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Data.Data.Entities.Config
{
    internal class ForumConfig : IEntityTypeConfiguration<Forum>
    {
        public void Configure(EntityTypeBuilder<Forum> builder)
        {
            builder.ToTable("Forums");
            builder.Property(f => f.Title).HasColumnType("VARCHAR").HasMaxLength(500);
            builder.HasOne(f => f.Course).WithMany(c => c.Forums).HasForeignKey(f => f.CourseId);
        }
    }
}
