using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Data.Data.Entities.Config
{
    internal class SubmissionConfig : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder.ToTable("Submissions");
            builder.Property(s => s.Content).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired();
            builder.Property(s => s.SubmissionDate).IsRequired();
            builder.HasOne(s => s.Assignment).WithMany(a => a.Submissions).HasForeignKey(s => s.AssignmentId);
            builder.HasOne(s => s.User).WithMany(u => u.Submissions).HasForeignKey(s => s.UserId);
        }
    }
}
