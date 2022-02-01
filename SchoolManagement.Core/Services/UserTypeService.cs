using AutoMapper;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserTypeModel>> GetAllUserTypes()
        {
            return _mapper.Map<List<UserTypeModel>>(await _unitOfWork.UserTypeRepository.GetAllAsync(o => o.OrderBy(ut => ut.CreationDate)) as List<UserType>);
        }

        public async Task<bool> Create(UserTypeModel model)
        {
            UserType userType = _mapper.Map<UserType>(model);
            userType.CreationDate = DateTime.Now;
            userType.Id = Guid.NewGuid();
            await _unitOfWork.UserTypeRepository.AddAsync(userType);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> ActivateType(Guid typeId)
        {
            UserType type = await _unitOfWork.UserTypeRepository.GetByIDAsync(typeId);
            if (type == null) return false;

            type.Active = !type.Active;
            await _unitOfWork.UserTypeRepository.UpdateAsync(type);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(params object[] arguments)
        {
            UserType userType = await _unitOfWork.UserTypeRepository.GetByIDAsync(arguments[0]);
            if (userType == null) return false;

            await _unitOfWork.UserTypeRepository.DeleteAsync(userType);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Edit(UserTypeModel model)
        {
            UserType userType = _mapper.Map<UserType>(model);

            await _unitOfWork.UserTypeRepository.UpdateAsync(userType);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public Task<UserTypeModel> Initiate(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public async Task<UserTypeModel> InitiateCreate(params object[] arguments)
        {
            return new UserTypeModel();
        }

        public async Task<UserTypeModel> InitiateEdit(params object[] arguments)
        {
            return _mapper.Map<UserTypeModel>(await _unitOfWork.UserTypeRepository.GetByIDAsync(arguments[0]));
        }
    }
}
