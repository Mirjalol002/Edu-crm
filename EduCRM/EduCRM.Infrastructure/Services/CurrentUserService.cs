using EduCRM.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EduCRM.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public int UserId { get; set; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var userClaims = httpContextAccessor.HttpContext.User.Claims;
            var idClaim = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            if (idClaim != null && int.TryParse(idClaim.Value, out int value))
            {
                UserId = value;
            }
        }
    }
}