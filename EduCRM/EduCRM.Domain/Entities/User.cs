using EduCRM.Domain.Enums;

namespace EduCRM.Domain.Entities
{
    public class User
    {
        public User()
        {
            Groups = new HashSet<Group>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string PhotoPath { get; set; } = string.Empty;


        public ICollection<Group> Groups { get; set; }
    }
}