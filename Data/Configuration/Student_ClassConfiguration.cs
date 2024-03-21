using Manager_Point.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager_Point.Configuration
{
    public class Student_ClassConfiguration : IEntityTypeConfiguration<Student_Class>
    {
        public void Configure(EntityTypeBuilder<Student_Class> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.Id).IsUnique();
            //User Id
            builder.Property(t => t.UserId);
            builder.HasIndex(t => t.UserId);
            //Class Id
            builder.Property(t => t.ClassId);
            builder.HasIndex(t => t.ClassId);

            //Set Relationship
            builder.HasOne<Class>(t => t.Class).WithMany(t => t.Student_Classes).HasForeignKey(t => t.ClassId);
            builder.HasOne<User>(t => t.User).WithMany(t => t.Student_Classes).HasForeignKey(t => t.UserId);
        }
    }
}
