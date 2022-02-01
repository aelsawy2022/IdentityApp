using AutoMapper;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.Models.Models.ViewModels;
using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SchoolService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Create(SchoolModel model)
        {
            School school = _mapper.Map<School>(model);
            school.CreationDate = DateTime.Now;
            school.Id = Guid.NewGuid();
            Management management = await _unitOfWork.ManagementRepository.GetByIDAsync(school.Management.Id);
            school.Management = management;
            school.Address.CreationDate = school.CreationDate;
            school.Address.Id = Guid.NewGuid();

            await _unitOfWork.AddressRepository.AddAsync(school.Address);
            await _unitOfWork.SchoolRepository.AddAsync(school);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(params object[] arguments)
        {
            School school = await _unitOfWork.SchoolRepository.GetByIDAsync(arguments[0]);
            if (school == null) return false;

            await _unitOfWork.SchoolRepository.DeleteAsync(school);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Edit(SchoolModel model)
        {
            School school = _mapper.Map<School>(model);
            school.Management = await _unitOfWork.ManagementRepository.GetByIDAsync(school.Management.Id);

            await _unitOfWork.AddressRepository.UpdateAsync(school.Address);
            await _unitOfWork.SchoolRepository.UpdateAsync(school);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<List<SchoolModel>> GetAllSchools()
        {
            var schools = await _unitOfWork.SchoolRepository.GetAllAsync(o => o.OrderBy(s => s.Name), "Address,Management") as List<School>;
            return _mapper.Map<List<SchoolModel>>(schools);
        }

        public Task<SchoolViewModel> Initiate(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public async Task<SchoolViewModel> InitiateCreate(params object[] arguments)
        {
            SchoolViewModel schoolViewModel = new SchoolViewModel();
            schoolViewModel.School = new SchoolModel();
            schoolViewModel.Managements = _mapper.Map<List<ManagementModel>>(await _unitOfWork.ManagementRepository.GetAllAsync() as List<Management>);

            return schoolViewModel;
        }

        public async Task<SchoolViewModel> InitiateEdit(params object[] arguments)
        {
            SchoolViewModel schoolViewModel = new SchoolViewModel();
            schoolViewModel.School = _mapper.Map<SchoolModel>(await _unitOfWork.SchoolRepository.GetOneAsync(e => e.Id == (Guid)arguments[0], "Address,Management"));
            schoolViewModel.Managements = _mapper.Map<List<ManagementModel>>(await _unitOfWork.ManagementRepository.GetAllAsync() as List<Management>);

            return schoolViewModel;
        }
    }
}
