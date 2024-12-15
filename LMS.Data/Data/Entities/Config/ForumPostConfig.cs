using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Data.Data.Entities.Config
{
    internal class ForumPostConfig : IEntityTypeConfiguration<ForumPost>
    {
        public void Configure(EntityTypeBuilder<ForumPost> builder)
        {
            builder.ToTable("ForumPosts");

            builder.Property(x => x.Content).HasColumnName("Content").HasMaxLength(300).IsRequired();
            builder.Property(x => x.PostDate).IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.ForumPosts).HasForeignKey(x => x.UserId);
        }
    }
}
