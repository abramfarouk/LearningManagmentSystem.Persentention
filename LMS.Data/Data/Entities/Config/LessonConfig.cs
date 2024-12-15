using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Data.Data.Entities.Config
{
    internal class LessonConfig : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lessons");
            builder.Property(l => l.Title).HasColumnType("VARCHAR").HasMaxLength(300).IsRequired();
            builder.Property(l => l.Content).HasColumnType("VARCHAR").HasMaxLength(300).IsRequired(false);

            builder.HasOne(l => l.Module).WithMany(m => m.Lessons).HasForeignKey(l => l.ModuleId);
        }
    }
}
