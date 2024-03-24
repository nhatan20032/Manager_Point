using Manager_Point.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager_Point.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.HasIndex(t => t.Id).IsUnique();

            //User code
            builder.Property(t => t.User_Code).HasMaxLength(10).IsUnicode(false);
            builder.HasIndex(t => t.User_Code).IsUnique();

            //Property
            builder.Property(t => t.Password).HasMaxLength(32).IsRequired();
            builder.Property(t => t.Name).HasMaxLength(100);
            builder.Property(t => t.Description).HasMaxLength(100);
            builder.Property(t => t.DOB);
            builder.Property(t => t.PhoneNumber).HasMaxLength(11);
            builder.Property(t => t.Gender).HasMaxLength(10);
            builder.Property(t => t.Email).HasMaxLength(100).IsUnicode(false);
            builder.Property(t => t.AvatarUrl);
            builder.Property(t => t.Address);
            builder.Property(t => t.Nation);
            builder.Property(t => t.Status).HasMaxLength(10);
        }
    }
}
