﻿using Data.Models;
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
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.HasIndex(t => t.Id).IsUnique();

            builder.Property(t => t.GradePointId);
            builder.HasIndex(t => t.GradePointId);


            builder.Property(t => t.Performance).HasMaxLength(5);
            builder.Property(t => t.Status).HasMaxLength(5);

            //Set Relationship
            builder.HasOne<GradePoint>(t => t.GradePoint).WithMany(t => t.AcademicPerformances).HasForeignKey(t => t.GradePointId);
        }
    }
}
