using AutoMapper;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services
{
    public class ActivityInstanceService : IActivityInstanceService
    {
        private readonly IRepository<ActivityInstance> _activityInstanceRepository;
        private readonly IMapper _mapper;

        public ActivityInstanceService(IRepository<ActivityInstance> activityInstanceRepository, IMapper mapper)
        {
            _activityInstanceRepository = activityInstanceRepository;
            _mapper = mapper;
        }

        public async Task<bool> Create(ActivityInstanceModel model)
        {
            model.CreationDate = DateTime.Now;
            await _activityInstanceRepository.AddAsync(_mapper.Map<ActivityInstance>(model));
            await _activityInstanceRepository.SaveAsync();

            return true;
        }

        public Task<bool> Delete(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(ActivityInstanceModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ActivityInstanceModel>> GetAllActivityInstances(Guid activityId)
        {
            List<ActivityInstance> activityInstances = await _activityInstanceRepository.GetAsync(ai => ai.ActivityId == activityId) as List<ActivityInstance>;
            return _mapper.Map<List<ActivityInstanceModel>>(activityInstances);
        }

        public async Task<ActivityInstanceModel> GetById(Guid instanceId)
        {
            return _mapper.Map<ActivityInstanceModel>(await _activityInstanceRepository.GetByIDAsync(instanceId));
        }

        public Task<ActivityInstanceModel> Initiate(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public Task<ActivityInstanceModel> InitiateCreate(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public Task<ActivityInstanceModel> InitiateEdit(params object[] arguments)
        {
            throw new NotImplementedException();
        }
    }
}
