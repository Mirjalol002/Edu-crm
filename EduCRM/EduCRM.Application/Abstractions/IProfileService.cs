using EduCRM.Application.Models;
using Microsoft.AspNetCore.Http;

namespace EduCRM.Application.Abstractions
{
    public interface IProfileService
    {
        Task SetPhoto(IFormFile formFile);
        Task<ProfileViewModel> GetProfile();
    }
}
