using SchoolManagementSystem.Domain.Entitites;
using SchoolManagementSystem.Domain.Repositories;
using SchoolManagementSystem.Domain.UnitOfWork;
using SchoolManagementSystem.DTOs;
using SchoolManagementSystem.Interfaces;

namespace SchoolManagementSystem.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork { get; set; }
        private IGenericRepository<User> _userRepository { get; set; }
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GenericRepository<User>();
        }
        public async Task<IEnumerable<User>> GetAll()
             => await _userRepository.GetAllAsync();

        public async Task<User> Get(int id)
              => await _userRepository.GetByIdAsync(id);

        public async Task<IEnumerable<User>> Add(UserDTO userDTO)
        {
            try
            {
                User user = new();
                user.RoleID = userDTO.RoleID;
                user.Username = userDTO.Username;
                user.Password = userDTO.Password;
                user.Email = userDTO.Email;

                await _userRepository.InsertAsync(user);
                await _unitOfWork.SaveAsync();
                return await _userRepository.GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<User>> Edit(UserDTO userDto)
        {

            try
            {
                var existingUser = await _userRepository.GetByIdAsync(userDto.UserID);
                if (existingUser == null)
                    throw new KeyNotFoundException($"User with ID {userDto.UserID} was not found.");
                
                existingUser.UserID = userDto.UserID;
                existingUser.RoleID = userDto.RoleID;
                existingUser.Username = userDto.Username;
                existingUser.Password = userDto.Password;
                existingUser.Email = userDto.Email;

                _userRepository.Update(existingUser);
                await _unitOfWork.SaveAsync();
                return await _userRepository.GetAllAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> Delete(int id)
        {
            try
            {
                User user = await this.Get(id);
                if (user == null)
                    throw new KeyNotFoundException($"User with ID {id} was not found.");

                _userRepository.Delete(user);
                await _unitOfWork.SaveAsync();
                return await _userRepository.GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
