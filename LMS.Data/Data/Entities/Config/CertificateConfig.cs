using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Data.Data.Entities.Config
{
    public class CertificateConfig : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.ToTable("Certifications");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.IssueDate).IsRequired();
            builder.HasOne(c => c.User).WithMany(u => u.Certificates).HasForeignKey(c => c.UserId)
            ;
            builder.HasOne(c => c.Course).WithMany(u => u.Certificates).HasForeignKey(c => c.CourseId)
            ;
        }

    }
}
