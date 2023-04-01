using EduCRM.Application.Abstractions;
using EduCRM.Application.Models;
using EduCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduCRM.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IApplicationDbContext _context;
        public StudentService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(CreateStudentModel entity)
        {
            var student = new Student()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
                BirthDate = entity.BirthDate,
                CreatedDateTime = DateTime.UtcNow,
                Description = entity.Description
            };
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (student == null)
            {
                throw new Exception("Not found");
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<List<StudentViewModel>> GetAllAsync()
        {
            var students = await _context.Students
                .Select(x => new StudentViewModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    BirthDate = x.BirthDate,
                    Description = x.Description
                }).ToListAsync();
            return students;
        }

        public async Task<StudentViewModel> GetByIdAsync(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (student == null)
            {
                throw new Exception("Not found");
            }
            return new StudentViewModel()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
                BirthDate = student.BirthDate,
                Description = student.Description
            };
        }

        public async Task UpdateAsync(UpdateStudentModel entity)
        {
            var studentId = await _context.Students.FirstOrDefaultAsync(x => x.Id == entity.Id);
            var student = new StudentViewModel()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
                BirthDate = entity.BirthDate,
                Description = entity.Description
            };

        }
    }
}
