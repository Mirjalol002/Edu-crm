using EduCRM.Application.Models;

namespace EduCRM.Application.Abstractions
{
    public interface IAttendanceService
    {
        Task CheckAsync(DoAttendanceCheckModel model);
    }
}
