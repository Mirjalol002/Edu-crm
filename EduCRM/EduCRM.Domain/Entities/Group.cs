namespace EduCRM.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TeacherId { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime EndDate { get; set; }
#pragma warning disable
        public User Teacher { get; set; }
#pragma warning restore
    }
}
