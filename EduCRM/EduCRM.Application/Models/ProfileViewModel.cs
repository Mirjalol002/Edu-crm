namespace EduCRM.Application.Models
{
    public class ProfileViewModel
    {
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;
#pragma warning disable
        public ICollection<GroupViewModel> groupViews { get; set; }
# pragma warning restore
    }
}
