using Manager_Point.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager_Point.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.Id).IsUnique();
            //Property
            builder.Property(t => t.Name).HasMaxLength(100);
            builder.Property(t => t.StartTime);
            builder.Property(t => t.EndTime);
            builder.Property(t => t.Description).HasMaxLength(1000);
            builder.Property(t => t.Status).HasMaxLength(5);
        }
    }
}
