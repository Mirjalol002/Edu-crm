using EduCRM.Application.Abstractions;
using EduCRM.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EduCRM.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHashProvider _hashProvider;
        private readonly ITokenService _tokenService;

        public AuthService(ApplicationDbContext dbContext, IHashProvider hashProvider, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _hashProvider = hashProvider;
            _tokenService = tokenService;
        }
        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (user.PasswordHash != _hashProvider.GetHash(password))
            {
                throw new Exception("Password is wrong");
            }
            return _tokenService.GenerateAccessToken(user);
        }
    }
}
