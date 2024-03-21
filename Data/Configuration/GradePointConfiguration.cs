using Manager_Point.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager_Point.Configuration
{
    public class GradePointConfiguration : IEntityTypeConfiguration<GradePoint>
    {
        public void Configure(EntityTypeBuilder<GradePoint> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.Id).IsUnique();

            builder.Property(t => t.SubjectId);
            builder.HasIndex(t => t.SubjectId);

            builder.Property(t => t.UserId);
            builder.HasIndex(t => t.UserId);

            builder.Property(t => t.Semester).HasMaxLength(5);
            builder.Property(t => t.Midterm_Grades).HasMaxLength(5);
            builder.Property(t => t.Final_Grades).HasMaxLength(5);
            builder.Property(t => t.Final_Grades).HasMaxLength(5);
        }
    }
}
