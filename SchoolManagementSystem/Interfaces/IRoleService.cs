using SchoolManagementSystem.Domain.Entitites;
using SchoolManagementSystem.DTOs;

namespace SchoolManagementSystem.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> Add(RoleDTO roleDto);
        Task<IEnumerable<Role>> Delete(int id);
        Task<IEnumerable<Role>> Edit(RoleDTO roleDto);
        Task<Role> Get(int id);
        Task<IEnumerable<Role>> GetAll();
    }
}