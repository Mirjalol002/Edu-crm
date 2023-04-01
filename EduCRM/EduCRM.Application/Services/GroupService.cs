using EduCRM.Application.Abstractions;
using EduCRM.Application.Models;
using EduCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduCRM.Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IApplicationDbContext _dbContext;
        public GroupService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddStudentAsync(AddStudentGroupModel groupModel, int groupId)
        {
            if (!await _dbContext.Students.AnyAsync(x => x.Id == groupModel.StudentId))
            {
                throw new Exception("Not found");
            }
            if (!await _dbContext.Groups.AnyAsync(x => x.Id == groupId))
            {
                throw new Exception("Not found");
            }
            var studentGroup = new StudentGroup()
            {
                Id = groupId,
                StudentId = groupModel.StudentId,
                IsPaid = groupModel.IsPaid,
                JoinedDate = groupModel.JoinedDate,
            };
            await _dbContext.StudentGroups.AddAsync(studentGroup);
            await _dbContext.SaveChangesAsync();
        }
        public async Task CreateAsync(CreateGroupModel entity)
        {
            var entities = new Group()
            {
                Name = entity.Name,
                TeacherId = entity.TeacherId,
                StartedDate = entity.StartDate.ToDateTime(TimeOnly.MinValue),
                EndDate = entity.EndDate.ToDateTime(TimeOnly.MaxValue)
            };
            _dbContext.Groups.Add(entities);
            var lessons = CreateLessons(entities, entity.LessonStartTime, entity.LessonEndTime);

            _dbContext.Lessons.AddRange(lessons);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (entity == null)
            {
                throw new Exception("Not found");
            }
            _dbContext.Groups.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<GroupViewModel>> GetAllAsync()
        {
            return await _dbContext.Groups
                .Select(x => new GroupViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    TeacherId = x.TeacherId,
                    StartDate = x.StartedDate,
                    EndDate = x.EndDate
                }).ToListAsync();
        }
        public async Task<GroupViewModel> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Groups.FirstOrDefaultAsync(x => x.Id == id);
            return new GroupViewModel()
            {
# pragma warning disable
                Id = entity.Id,
# pragma warning restore
                Name = entity.Name,
                TeacherId = entity.TeacherId,
                StartDate = entity.StartedDate,
                EndDate = entity.EndDate
            };
        }
        public async Task<List<LessonViewModel>> GetLessonAsync(int groupId)
        {
            var lessons = await _dbContext.Lessons.Where(x => x.GroupId == groupId)
                .Select(x => new LessonViewModel()
                {
                    Id = x.Id,
                    GroupId = x.GroupId,
                    StartDateTime = x.StartedDateTime,
                    EndDateTime = x.EndedDateTime
                }).ToListAsync();
            return lessons;
        }
        public async Task RemoveStudentAsync(int studentId, int groupId)
        {
            var entity = await _dbContext.StudentGroups.FirstOrDefaultAsync(x => x.StudentId == studentId && x.GroupId == groupId);
            if (entity == null)
            {
                throw new Exception("Not found");
            }
            _dbContext.StudentGroups.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(UpdateGroupModel entity)
        {
            var group = await _dbContext.Groups.Include(x => x.Lessons).FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (group == null)
            {
                throw new Exception("Not found");
            }
            group.Name = entity.Name ?? group.Name;
            group.TeacherId = entity.TeacherId ?? group.TeacherId;

            _dbContext.Groups.Update(group);
            await _dbContext.SaveChangesAsync();
        }
        private List<Lesson> CreateLessons(Group entity, TimeSpan lessonStartTime, TimeSpan lessonEndTime)
        {
            var lessons = new List<Lesson>();
            var totalDaysFromStartToEnd = (entity.EndDate - entity.StartedDate).Days;
            var currentDate = entity.StartedDate;
            for (int i = 1; i < totalDaysFromStartToEnd; i++)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    var lesson = new Lesson()
                    {
                        Group = entity,
                        StartedDateTime = currentDate.Date + lessonStartTime,
                        EndedDateTime = currentDate.Date + lessonEndTime
                    };
                    lessons.Add(lesson);
                }
                currentDate = currentDate.AddDays(1);
            }
            return lessons;
        }
    }
}