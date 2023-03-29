using EduCRM.Application.Abstractions;
using EduCRM.Application.Models;
using EduCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace EduCRM.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        public AttendanceService(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }
        public async Task CheckAsync(DoAttendanceCheckModel model)
        {
            var lesson = await _dbContext.Lessons.Include(c => c.Group).FirstOrDefaultAsync(c => c.Id == model.LessonId);
#pragma warning disable
            if (lesson == null && lesson.Group.TeacherId != _currentUserService.UserId)
            {
# pragma warning restore
                throw new Exception("Not found");
            }
            var groupStudent = await _dbContext.Lessons
                .Where(x=>x.Id == model.LessonId)
                .Include(x=>x.Group)
                .ThenInclude(x=>x.StudentGroups)
                .SelectMany(x=>x.Group.StudentGroups)
                .Select(x=>x.StudentId)
                .ToListAsync();
            var attendanceList = new List<Attendance>();

            foreach (var studentId in groupStudent)
            {
                var check = model.checkModels.FirstOrDefault(x => x.StudentId == studentId);
                var attendance = new Attendance()
                {
                    StudentId = studentId,
                    LessonId = model.LessonId,
                    HasParticipated = false
                };
                if (check != null)
                {
                    attendance.HasParticipated = check.HasParticipated;
                }
                attendanceList.Add(attendance);
            }
            _dbContext.Attendances.AddRange(attendanceList);
            await _dbContext.SaveChangesAsync();
        }
    }
}