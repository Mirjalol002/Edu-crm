using EduCRM.Application.Models;

namespace EduCRM.Application.Abstractions
{
    public interface ITeacherService : ICrudService<int, TeacherViewModel, CreateTeacherModel, UpdateTeacherModel>
    {
    }
}
