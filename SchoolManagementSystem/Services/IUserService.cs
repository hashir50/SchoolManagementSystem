using SchoolManagementSystem.Domain.Entitites;
using SchoolManagementSystem.DTOs;

namespace SchoolManagementSystem.Services
{
  public interface IUserService
  {
        Task<IEnumerable<User>> Add(UserDTO user);
        Task<User> Get(int id);
        Task<IEnumerable<User>> GetAll();
  }
}
