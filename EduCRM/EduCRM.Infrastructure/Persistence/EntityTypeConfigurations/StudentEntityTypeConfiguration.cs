using EduCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduCRM.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(t => t.LastName)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}