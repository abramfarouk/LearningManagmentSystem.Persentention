using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Data.Data.Entities.Config
{
    internal class ModuleConfig : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("Modules");

            builder.Property(m => m.Title).HasColumnType("VARCHAR").HasMaxLength(300).IsRequired();
            builder.Property(m => m.Description).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired(false);
            builder.HasOne(m => m.Course).WithMany(c => c.Modules).HasForeignKey(m => m.CourseId);
        }
    }
}
