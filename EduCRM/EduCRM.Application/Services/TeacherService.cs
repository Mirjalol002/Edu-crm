using AutoMapper;
using EduCRM.Application.Abstractions;
using EduCRM.Application.Models;
using EduCRM.Domain.Entities;
using EduCRM.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EduCRM.Application.Services
{
    public class TeacherService : ITeacherService
    {
        IApplicationDbContext _context;
        IHashProvider _hashProvider;
        IMapper _mapper;
        public TeacherService(IApplicationDbContext context, IHashProvider hashProvider, IMapper mapper)
        {
            _context = context;
            _hashProvider = hashProvider;
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateTeacherModel entity)
        {
            var teacher = _mapper.Map<User>(entity);
            await _context.Users.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var teacher = await _context.Users.FirstOrDefaultAsync(x=>x.Id == id && x.Role == UserRole.Teacher);
            if (teacher == null)
            {
                throw new Exception("Not found");
            }
            _context.Users.Remove(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TeacherViewModel>> GetAllAsync()
        {
            var teacher = await _context.Users.Where(x=>x.Role == UserRole.Teacher).ToListAsync();
            return _mapper.Map<List<TeacherViewModel>>(teacher);
        }

        public async Task<TeacherViewModel> GetByIdAsync(int id)
        {
            var teacher = await _context.Users.FirstOrDefaultAsync(x => x.Id == id && x.Role == UserRole.Teacher);
            return _mapper.Map<TeacherViewModel>(teacher);
        }

        public async Task UpdateAsync(UpdateTeacherModel entity)
        {
            var teacher = await _context.Users.FirstOrDefaultAsync(x => x.Id == entity.Id && x.Role == UserRole.Teacher);
            if (teacher == null)
            {
                throw new Exception("Not found");
            }
            teacher = _mapper.Map(entity, teacher);
            _context.Users.Update(teacher);
            await _context.SaveChangesAsync();
        }
    }
}