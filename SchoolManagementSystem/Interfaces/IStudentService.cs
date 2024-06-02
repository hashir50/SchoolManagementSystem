using SchoolManagementSystem.Domain.Entitites;

namespace SchoolManagementSystem.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAll();
    }
}
