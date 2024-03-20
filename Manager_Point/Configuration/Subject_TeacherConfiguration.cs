using Manager_Point.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager_Point.Configuration
{
    public class Subject_TeacherConfiguration : IEntityTypeConfiguration<Subject_Teacher>
    {
        public void Configure(EntityTypeBuilder<Subject_Teacher> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.Id).IsUnique();
            //User Id
            builder.Property(t => t.UserId);
            builder.HasIndex(t => t.UserId);
            //Class Id
            builder.Property(t => t.SubjectId);
            builder.HasIndex(t => t.SubjectId);

            //Set Relationship
            builder.HasOne<Subject>(t => t.Subject).WithMany(t => t.Subject_Teachers).HasForeignKey(t => t.SubjectId);
            builder.HasOne<User>(t => t.User).WithMany(t => t.Subject_Teachers).HasForeignKey(t => t.UserId);
        }
    }
}
