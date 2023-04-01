using EduCRM.Application.Abstractions;
using EduCRM.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduCRM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        [HttpPost("Check")]
        public async Task<IActionResult> Check(DoAttendanceCheckModel checkModel)
        {
            await _attendanceService.CheckAsync(checkModel);
            return Ok();
        }
    }
}
