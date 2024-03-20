using Manager_Point.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager_Point.Configuration
{
    public partial class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.Id).IsUnique();
            //Grade Id
            builder.Property(t => t.GradeId);
            builder.HasIndex(t => t.GradeId);
            //Class Code
            builder.Property(t => t.ClassCode).IsRequired().HasMaxLength(10);
            builder.HasIndex(t => t.ClassCode).IsUnique();
            //Property
            builder.Property(t => t.Name).HasMaxLength(100);

            //Set Relationship
            builder.HasOne<GradeLevel>(t => t.GradeLevel).WithMany(t => t.Classes).HasForeignKey(t => t.GradeId);
        }
    }
}
