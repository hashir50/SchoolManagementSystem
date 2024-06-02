using SchoolManagementSystem.Domain.Entitites;
using SchoolManagementSystem.DTOs;

namespace SchoolManagementSystem.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> Add(UserDTO user);
        Task<IEnumerable<User>> Delete(int id);
        Task<IEnumerable<User>> Edit(UserDTO userDto);
        Task<User> Get(int id);
        Task<IEnumerable<User>> GetAll();
    }
}
