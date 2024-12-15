using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Data.Data.Entities.Config
{
    internal class UserRefreshTokenConfig : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable("UserRefreshTokens");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(ur => ur.User).WithMany(u => u.UserRefreshTokens).HasForeignKey(ur => ur.UserId);
        }
    }
}
