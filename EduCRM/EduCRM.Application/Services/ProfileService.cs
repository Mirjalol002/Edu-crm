using EduCRM.Application.Abstractions;
using EduCRM.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EduCRM.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IFileService _fileService;
        public ProfileService(IApplicationDbContext context, ICurrentUserService currentUserService, IFileService fileService)
        {
            _context = context;
            _currentUserService = currentUserService;
            _fileService = fileService;
        }
        public async Task<ProfileViewModel> GetProfile()
        {
            var userId = _currentUserService.UserId;
            var user = await _context.Users.Include(x => x.Groups).FirstOrDefaultAsync(x => x.Id == userId);

            return new ProfileViewModel()
            {
# pragma warning disable
                FirstName = user.FirstName,
# pragma warning restore
                LastName = user.LastName,
                UserName = user.UserName,
                PhotoPath = user.PhotoPath,
                groupViews = new List<GroupViewModel>(user.Groups.Select(x => new GroupViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    TeacherId = x.TeacherId,
                    StartDate = x.StartedDate,
                    EndDate = x.EndDate
                }))
            };
        }

        public async Task SetPhoto(IFormFile formFile)
        {
            var userId = _currentUserService.UserId;
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new Exception("Not found");
            }
            var path = await _fileService.Upload(formFile);
            user.PhotoPath = path;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}