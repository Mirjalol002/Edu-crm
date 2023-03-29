namespace EduCRM.Application.Models
{
    public class DoAttendanceCheckModel
    {
        public int LessonId { get; set; }
#pragma warning disable
        public List<AttendanceCheckModel> checkModels { get; set; }
# pragma warning restore
    }
}
