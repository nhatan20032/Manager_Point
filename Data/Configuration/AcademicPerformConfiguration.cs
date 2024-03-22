using Data.Models;
using Manager_Point.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class AcademicPerformConfiguration : IEntityTypeConfiguration<AcademicPerformance>
    {
        public void Configure(EntityTypeBuilder<AcademicPerformance> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.Id).IsUnique();

            builder.Property(t => t.UserId);
            builder.HasIndex(t => t.UserId);

            builder.Property(t => t.GradePointId);
            builder.HasIndex(t => t.GradePointId);


            builder.Property(t => t.Performance).HasMaxLength(5);
            builder.Property(t => t.Status).HasMaxLength(5);

            //Set Relationship
            builder.HasOne<User>(t => t.User).WithMany(t => t.AcademicPerformances).HasForeignKey(t => t.UserId);
            builder.HasOne<GradePoint>(t => t.GradePoint).WithMany(t => t.AcademicPerformances).HasForeignKey(t => t.GradePointId);
        }
    }
}
