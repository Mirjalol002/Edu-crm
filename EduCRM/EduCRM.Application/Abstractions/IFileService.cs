using Microsoft.AspNetCore.Http;

namespace EduCRM.Application.Abstractions
{
    public interface IFileService
    {
        Task<string> Upload(IFormFile fromFile);
    }
}
