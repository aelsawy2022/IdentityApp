using AutoMapper;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.Models.Models.ViewModels;
using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services
{
    public class SchoolRoleService : ISchoolRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SchoolRoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> ActivateRole(Guid roleId)
        {
            Role role = await _unitOfWork.RoleRepository.GetByIDAsync(roleId);
            if (role == null) return false;

            role.Active = !role.Active;

            await _unitOfWork.RoleRepository.UpdateAsync(role);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Create(RolesModel model)
        {
            Role role = _mapper.Map<Role>(model);
            role.Id = Guid.NewGuid();
            role.Name = role.Name.Trim().Replace(" ", "");
            role.School = await _unitOfWork.SchoolRepository.GetByIDAsync(role.School.Id);

            await _unitOfWork.RoleRepository.AddAsync(role);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(params object[] arguments)
        {
            Role role = await _unitOfWork.RoleRepository.GetByIDAsync(arguments[0]);

            if (role == null) return false;

            await _unitOfWork.RoleRepository.DeleteAsync(role);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Edit(RolesModel model)
        {
            Role _role = await _unitOfWork.RoleRepository.GetByIDAsync(model.Id);

            _role.Name = model.Name.Trim().Replace(" ", "");
            _role.Active = model.Active;

            _role.School = await _unitOfWork.SchoolRepository.GetByIDAsync(model.School.Id);

            await _unitOfWork.RoleRepository.UpdateAsync(_role);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<RoleViewModel> Initiate(params object[] arguments)
        {
            RoleViewModel roleViewModel = new RoleViewModel();

            roleViewModel.Roles = _mapper.Map<List<RolesModel>>(await _unitOfWork.RoleRepository.GetAsync(r => r.School.Id == (Guid)arguments[0]) as List<Role>);
            roleViewModel.School = _mapper.Map<SchoolModel>(await _unitOfWork.SchoolRepository.GetByIDAsync(arguments[0]));

            return roleViewModel;
        }

        public async Task<RoleViewModel> InitiateCreate(params object[] arguments)
        {
            RoleViewModel roleViewModel = new RoleViewModel();
            roleViewModel.Role = new RolesModel();
            roleViewModel.School = _mapper.Map<SchoolModel>(await _unitOfWork.SchoolRepository.GetByIDAsync(arguments[0]));

            return roleViewModel;
        }

        public async Task<RoleViewModel> InitiateEdit(params object[] arguments)
        {
            RoleViewModel roleViewModel = new RoleViewModel();
            roleViewModel.Role = _mapper.Map<RolesModel>(await _unitOfWork.RoleRepository.GetByIDAsync(arguments[0]));
            roleViewModel.School = _mapper.Map<SchoolModel>(await _unitOfWork.SchoolRepository.GetByIDAsync(arguments[1]));

            return roleViewModel;
        }
    }
}
