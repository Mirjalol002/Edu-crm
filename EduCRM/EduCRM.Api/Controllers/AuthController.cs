using EduCRM.Api.Models;
using EduCRM.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EduCRM.Api.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            var token = await _authService.LoginAsync(loginRequest.UserName, loginRequest.Password);
            return Ok(token);
        }

    }
}
