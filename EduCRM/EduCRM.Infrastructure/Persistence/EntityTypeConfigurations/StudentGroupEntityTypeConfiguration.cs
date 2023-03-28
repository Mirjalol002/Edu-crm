using EduCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduCRM.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class StudentGroupEntityTypeConfiguration : IEntityTypeConfiguration<StudentGroup>
    {
        public void Configure(EntityTypeBuilder<StudentGroup> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Student)
                .WithMany(t => t.StudentGroups)
                .HasForeignKey(t => t.StudentId);

            builder.HasOne(t => t.Group)
                .WithMany(t => t.StudentGroups)
                .HasForeignKey(t => t.GroupId);
        }
    }
}