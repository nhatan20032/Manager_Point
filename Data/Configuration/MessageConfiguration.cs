using Manager_Point.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager_Point.Configuration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.HasIndex(t => t.Id).IsUnique();
            //Class Id
            builder.Property(t => t.ClassId);
            builder.HasIndex(t => t.ClassId);
            //Property
            builder.Property(t => t.Content).HasMaxLength(1000);
            builder.Property(t => t.Status).HasMaxLength(5);

            //Set Relationship
            builder.HasOne<Class>(t => t.Class).WithMany(t => t.Messages).HasForeignKey(t => t.ClassId);
        }
    }
}
