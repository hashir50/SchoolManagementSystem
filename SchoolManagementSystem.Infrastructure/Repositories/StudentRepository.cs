using SchoolManagementSystem.Domain.Entitites;
using SchoolManagementSystem.Infrastructure.DBContext;

namespace SchoolManagementSystem.Infrastructure.Repositories
{
  public class StudentRepository : GenericRepository<Student>, IStudentRepository
  {
    public StudentRepository(ApplicationContext dbContext) : base(dbContext)
    {
    }

   
  }
}
