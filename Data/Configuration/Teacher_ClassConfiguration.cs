using Manager_Point.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager_Point.Configuration
{
    public class Teacher_ClassConfiguration : IEntityTypeConfiguration<Teacher_Class>
    {
        public void Configure(EntityTypeBuilder<Teacher_Class> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.HasIndex(t => t.Id).IsUnique();
            //User Id
            builder.Property(t => t.UserId);
            builder.HasIndex(t => t.UserId);
            //Class Id
            builder.Property(t => t.ClassId);
            builder.HasIndex(t => t.ClassId);
            //Property
            builder.Property(t => t.TypeTeacher);
            //Set Relationship
            builder.HasOne<Class>(t => t.Class).WithMany(t => t.Teacher_Classes).HasForeignKey(t => t.ClassId);
            builder.HasOne<User>(t => t.User).WithMany(t => t.Teacher_Classes).HasForeignKey(t => t.UserId);
        }
    }
}
