using EduCRM.Application.Models;

namespace EduCRM.Application.Abstractions
{
    public interface IGroupService : ICrudService<int, GroupViewModel, CreateGroupModel, UpdateGroupModel>
    {
        Task<List<LessonViewModel>> GetLessonAsync(int groupId);
        Task AddStudentAsync(AddStudentGroupModel groupModel, int groupId);
        Task RemoveStudentAsync(int studentId, int groupId);
    }
}
