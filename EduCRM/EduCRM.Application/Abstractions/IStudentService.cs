using EduCRM.Application.Models;

namespace EduCRM.Application.Abstractions
{
    public interface IStudentService : ICrudService<int, StudentViewModel, CreateStudentModel, UpdateStudentModel>
    {
    }
}