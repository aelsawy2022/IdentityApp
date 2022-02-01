using AutoMapper;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.Models.Models.ViewModels;
using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.Repositories.GovernorateRepo;
using SchoolManagement.Persistance.Repositories.ManagementRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services
{
    public class ManagementService : IManagementService
    {
        private readonly IManagementRepository _managementRepository;
        private readonly IGovernorateRepository _governorateRepository;
        private readonly IMapper _mapper;

        public ManagementService(
            IManagementRepository managementRepository,
            IGovernorateRepository governorateRepository,
            IMapper mapper
            )
        {
            _managementRepository = managementRepository;
            _governorateRepository = governorateRepository;
            _mapper = mapper;
        }

        public async Task<bool> Create(ManagementModel model)
        {
            Management management = _mapper.Map<Management>(model);
            management.CreationDate = DateTime.Now;
            management.Id = Guid.NewGuid();
            management.Governorate = await _governorateRepository.GetByIDAsync(management.Governorate.Id);

            await _managementRepository.AddAsync(management);
            await _managementRepository.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(params object[] arguments)
        {
            Management management = await _managementRepository.GetByIDAsync(arguments[0]);

            if (management == null) return false;

            await _managementRepository.DeleteAsync(management);
            await _managementRepository.SaveAsync();

            return true;
        }

        public async Task<bool> Edit(ManagementModel model)
        {
            Management management = _mapper.Map<Management>(model);
            management.Governorate = await _governorateRepository.GetByIDAsync(management.Governorate.Id);

            await _managementRepository.UpdateAsync(management);
            await _managementRepository.SaveAsync();

            return true;
        }

        public async Task<ManagementViewModel> Initiate(params object[] arguments)
        {
            ManagementViewModel managementViewModel = new ManagementViewModel();
            managementViewModel.Managements = _mapper.Map<List<ManagementModel>>(await _managementRepository.GetAllAsync(o => o.OrderBy(m => m.CreationDate), "Governorate") as List<Management>);

            return managementViewModel;
        }

        public async Task<ManagementViewModel> InitiateCreate(params object[] arguments)
        {
            ManagementViewModel managementViewModel = new ManagementViewModel();
            managementViewModel.Management = new ManagementModel();
            managementViewModel.Governorates = _mapper.Map<List<GovernorateModel>>(await _governorateRepository.GetAllAsync() as List<Governorate>);

            return managementViewModel;
        }

        public async Task<ManagementViewModel> InitiateEdit(params object[] arguments)
        {
            ManagementViewModel managementViewModel = new ManagementViewModel();
            managementViewModel.Management = _mapper.Map<ManagementModel>(await _managementRepository.GetOneAsync(m => m.Id == (Guid)arguments[0], "Governorate"));
            managementViewModel.Governorates = _mapper.Map<List<GovernorateModel>>(await _governorateRepository.GetAllAsync() as List<Governorate>);

            return managementViewModel;
        }
    }
}
