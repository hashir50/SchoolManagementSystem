﻿using SchoolManagementSystem.Domain.Entitites;
using SchoolManagementSystem.Domain.Repositories;
using SchoolManagementSystem.Domain.UnitOfWork;
using SchoolManagementSystem.DTOs;
using SchoolManagementSystem.Interfaces;

namespace SchoolManagementSystem.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Role> _roleRepository;

        public RoleService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            _roleRepository = unitOfWork.GenericRepository<Role>();
        }
        public async Task<IEnumerable<Role>> GetAll()
             => await this.GetAll();

        public async Task<Role> Get(int id)
        {
             return await _roleRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Role>> Add(RoleDTO roleDto)
        {
            try
            {
                Role role = new();
                role.RoleID = roleDto.RoleID;
                role.RoleName = roleDto.RoleName;
                role.Description = roleDto.Description;

                await _roleRepository.InsertAsync(role);
                await _unitOfWork.SaveAsync();
                return await this.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Role>> Edit(RoleDTO roleDto)
        {
            try
            {
                var existingRole = await _roleRepository.GetByIdAsync(roleDto.RoleID);
                if (existingRole == null)
                {
                    throw new KeyNotFoundException($"Role with ID {roleDto.RoleID} was not found.");
                }

                existingRole.RoleName = roleDto.RoleName;
                existingRole.Description = roleDto.Description;

                _roleRepository.Update(existingRole);
                await _unitOfWork.SaveAsync();

                return await this.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Role>> Delete(int id)
        {
            try
            {
                Role role = await this.Get(id);
                if (role == null)
                {
                    throw new KeyNotFoundException($"Role with ID {id} was not found.");
                }
                _roleRepository.Delete(role);
                await _unitOfWork.SaveAsync();
                return await this.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
