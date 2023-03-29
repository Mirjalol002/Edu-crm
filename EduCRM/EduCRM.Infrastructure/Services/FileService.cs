using EduCRM.Application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace EduCRM.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public Task<string> Upload(IFormFile fromFile)
        {
            throw new NotImplementedException();
        }
    }
}
