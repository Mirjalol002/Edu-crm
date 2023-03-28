using EduCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduCRM.Application.Abstractions
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Attendance> Attendances { get; set; }
        DbSet<Lesson> Lessons { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<StudentGroup> StudentGroups { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
