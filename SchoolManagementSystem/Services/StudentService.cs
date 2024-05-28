using SchoolManagementSystem.Domain.Entitites;
using SchoolManagementSystem.Domain.UnitOfWork;

namespace SchoolManagementSystem.Services
{
  public class StudentService :IStudentService
  {
    private IUnitOfWork _unitOfWork { get; set; }
    public StudentService(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Student>> GetAll()
          => await _unitOfWork.GenericRepository<Student>().GetAllAsync();
  }
}
