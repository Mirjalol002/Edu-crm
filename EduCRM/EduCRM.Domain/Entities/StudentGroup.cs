namespace EduCRM.Domain.Entities
{
    public class StudentGroup
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public DateTime JoinedDate { get; set; }
        public bool IsPaid { get; set; }

    }
}