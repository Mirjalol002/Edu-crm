namespace EduCRM.Application.Models
{
    public class UpdateGroupModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? TeacherId { get; set; }
        //public DateOnly StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        //public TimeSpan LessonStartTime { get; set; }
        //public TimeSpan LessonEndTime { get; set; }
    }
}
