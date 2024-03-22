using Manager_Point.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager_Point.Configuration
{
    public class ExaminationConfiguration : IEntityTypeConfiguration<Examination>
    {
        public void Configure(EntityTypeBuilder<Examination> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.HasIndex(t => t.Id).IsUnique();
            //Grade Id
            builder.Property(t => t.GradePointId);
            builder.HasIndex(t => t.GradePointId);
            //Property
            builder.Property(t => t.Point).HasMaxLength(10);
            builder.Property(t => t.TypePoint).HasMaxLength(5);

            //Set Relationship
            builder.HasOne<GradePoint>(t => t.GradePoint).WithMany(t => t.Examinations).HasForeignKey(t => t.GradePointId);
        }
    }
}
