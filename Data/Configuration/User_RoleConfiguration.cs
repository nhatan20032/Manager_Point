using Manager_Point.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager_Point.Configuration
{
    public class User_RoleConfiguration : IEntityTypeConfiguration<User_Role>
    {
        public void Configure(EntityTypeBuilder<User_Role> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.Id).IsUnique();
            //User Id
            builder.Property(t => t.UserId);
            builder.HasIndex(t => t.UserId);
            //Class Id
            builder.Property(t => t.RoleId);
            builder.HasIndex(t => t.RoleId);

            //Set Relationship
            builder.HasOne<Role>(t => t.Role).WithMany(t => t.User_Roles).HasForeignKey(t => t.RoleId);
            builder.HasOne<User>(t => t.User).WithMany(t => t.User_Roles).HasForeignKey(t => t.UserId);
        }
    }
}
