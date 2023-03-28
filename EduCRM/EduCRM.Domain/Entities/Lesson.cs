namespace EduCRM.Domain.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public DateTime StartedDateTime { get; set; }
        public DateTime EndedDateTime { get; set; }
        public int GroupId { get; set; }
        public bool IsDone { get; set; }
        public Group Group { get; set; }
    }
}