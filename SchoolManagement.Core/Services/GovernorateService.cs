using AutoMapper;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.Repositories.GovernorateRepo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services
{
    public class GovernorateService : IGovernorateService
    {
        private readonly IGovernorateRepository _governorateRepository;
        private readonly IMapper _mapper;
        public GovernorateService(
            IGovernorateRepository governorateRepository,
            IMapper mapper
            )
        {
            _governorateRepository = governorateRepository;
            _mapper = mapper;
        }

        public async Task<bool> Create(GovernorateModel model)
        {
            Governorate governorate = _mapper.Map<Governorate>(model);
            governorate.CreationDate = DateTime.Now;
            governorate.Id = Guid.NewGuid();

            await _governorateRepository.AddAsync(governorate);
            await _governorateRepository.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(params object[] arguments)
        {
            Governorate governorate = await _governorateRepository.GetByIDAsync(arguments[0]);

            if (governorate == null)
            {
                return false;
            }

            await _governorateRepository.DeleteAsync(governorate);
            await _governorateRepository.SaveAsync();

            return true;
        }

        public async Task<bool> Edit(GovernorateModel model)
        {
            Governorate governorate = _mapper.Map<Governorate>(model);

            await _governorateRepository.UpdateAsync(governorate);
            await _governorateRepository.SaveAsync();

            return true;
        }

        public async Task<List<GovernorateModel>> GetAllGovernorates()
        {
            return _mapper.Map<List<GovernorateModel>>(await _governorateRepository.GetAllAsync() as List<Governorate>);
        }

        public Task<GovernorateModel> Initiate(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public async Task<GovernorateModel> InitiateCreate(params object[] arguments)
        {
            return new GovernorateModel();
        }

        public async Task<GovernorateModel> InitiateEdit(params object[] arguments)
        {
            return _mapper.Map<GovernorateModel>(await _governorateRepository.GetByIDAsync(arguments[0]));
        }
    }
}
