using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Data.Data.Entities.Config
{
    internal class GradeConfig : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("Grades");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(g => g.grade).IsRequired();
            builder.HasOne(g => g.Submission).WithOne(s => s.grade).HasForeignKey<Grade>(g => g.SubmissionId);
        }
    }
}
