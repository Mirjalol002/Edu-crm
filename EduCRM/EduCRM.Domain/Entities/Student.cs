namespace EduCRM.Domain.Entities
{
    public class Student
    {
        public Student()
        {
            Attendances = new HashSet<Attendance>();
            StudentGroups = new HashSet<StudentGroup>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Description { get; set; } = string.Empty;

        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<StudentGroup> StudentGroups { get; set; }
    }
}