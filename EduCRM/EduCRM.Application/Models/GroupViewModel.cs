namespace EduCRM.Application.Models
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TeacherId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
