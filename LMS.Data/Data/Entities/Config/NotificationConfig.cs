using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Data.Data.Entities.Config
{
    internal class NotificationConfig : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(n => n.Message).HasMaxLength(1000).HasColumnType("VARCHAR").IsRequired();
            builder.Property(n => n.SendDate).IsRequired();
            builder.HasOne(n => n.User).WithMany(u => u.Notifications).HasForeignKey(n => n.UserId);
        }
    }
}
