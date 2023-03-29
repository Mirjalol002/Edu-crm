using EduCRM.Domain.Entities;

namespace EduCRM.Application.Abstractions
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
    }
}
