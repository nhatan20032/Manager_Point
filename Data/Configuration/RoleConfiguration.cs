using Manager_Point.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager_Point.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.HasIndex(t => t.Id).IsUnique();
            //Property
            builder.Property(t => t.Name).HasMaxLength(128);
            builder.Property(t => t.Role_Code).HasMaxLength(10);
            builder.Property(t => t.Description).HasMaxLength(200);
            builder.Property(t => t.Status).HasMaxLength(10);
        }
    }
}
