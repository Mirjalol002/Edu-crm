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
        public Task AddStudentAsync(AddStudentGroupModel groupModel, int groupId)
        {
            throw new NotImplementedException();
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
            var entity = await _dbContext.Groups.FirstOrDefaultAsync(x=>x.Id == id);
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
        public Task<List<LessonViewModel>> GetLessonAsync(int groupId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveStudentAsync(int studentId, int groupId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UpdateGroupModel entity)
        {
            throw new NotImplementedException();
        }
        private List<Lesson> CreateLessons(Group entity, TimeSpan lessonStartTime,  TimeSpan lessonEndTime)
        {
            var lessons = new List<Lesson>();
            var totalDaysFromStartToEnd = (entity.EndDate - entity.StartedDate).Days;
            var currentDate = entity.StartedDate;
            for (int i = 1; i < totalDaysFromStartToEnd; i++)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek!= DayOfWeek.Sunday)
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
