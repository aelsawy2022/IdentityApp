using AutoMapper;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.ViewModels.ViewModels;
using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.Repositories.ActivityRepo;
using SchoolManagement.Persistance.Repositories.RoleRepo;
using SchoolManagement.Persistance.Repositories.SchoolRepo;
using SchoolManagement.Persistance.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActivityService(
            IActivityRepository activityRepository,
            ISchoolRepository schoolRepository,
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _activityRepository = activityRepository;
            _schoolRepository = schoolRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ActivateActivity(params object[] arguments)
        {
            Persistance.Data.Entities.Activity activity = await _unitOfWork.ActivityRepository.GetByIDAsync(arguments[0]);
            if (activity == null)
            {
                return false;
            }
            activity.Active = !activity.Active;

            await _unitOfWork.ActivityRepository.UpdateAsync(activity);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Create(Models.Models.ActivityModel model)
        {
            Persistance.Data.Entities.Activity activity = _mapper.Map<Persistance.Data.Entities.Activity>(model);

            activity.CreationDate = DateTime.Now;
            activity.Id = Guid.NewGuid();
            activity.School = await _unitOfWork.SchoolRepository.GetByIDAsync(activity.School.Id);

            Role role = new Role() { Id = Guid.NewGuid(), Name = activity.Name, Active = true, School = activity.School };
            await _unitOfWork.RoleRepository.AddAsync(role);

            if (activity.Roles == null) activity.Roles = new List<Role>();

            activity.Roles.Add(role);

            await _unitOfWork.ActivityRepository.AddAsync(activity);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(params object[] arguments)
        {
            Persistance.Data.Entities.Activity activity = (await _unitOfWork.ActivityRepository.GetAsync(a => a.Id == (Guid)arguments[0], null, "Roles")).FirstOrDefault();

            if (activity == null)
            {
                return false;
            }

            if (activity.Roles != null)
            {
                foreach (Role role in activity.Roles)
                {
                    await _unitOfWork.RoleRepository.DeleteAsync(role);
                }
            }
            await _unitOfWork.ActivityRepository.DeleteAsync(activity);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Edit(Models.Models.ActivityModel model)
        {
            Persistance.Data.Entities.Activity activity = _mapper.Map<Persistance.Data.Entities.Activity>(model);
            activity.School = await _unitOfWork.SchoolRepository.GetByIDAsync(activity.School.Id);

            Role role = await _unitOfWork.RoleRepository.GetOneAsync(r => r.School.Id == activity.School.Id && r.Activity.Id == activity.Id);

            if (role != null)
            {
                role.Name = activity.Name;
                await _unitOfWork.RoleRepository.UpdateAsync(role);
            }

            await _unitOfWork.ActivityRepository.UpdateAsync(activity);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<ActivityVM> Initiate(params object[] arguments)
        {
            ActivityVM activityViewModel = new ActivityVM();
            activityViewModel.Activities = _mapper.Map<List<Models.Models.ActivityModel>>(await _activityRepository.GetAsync(a => a.School.Id == (Guid)arguments[0], o => o.OrderBy(a => a.CreationDate)) as List<Persistance.Data.Entities.Activity>);
            activityViewModel.School = _mapper.Map<SchoolModel>(await _schoolRepository.GetByIDAsync(arguments[0]));
            return activityViewModel;
        }

        public async Task<ActivityVM> InitiateCreate(params object[] arguments)
        {
            ActivityVM activityViewModel = new ActivityVM();
            activityViewModel.Activity = new Models.Models.ActivityModel();
            activityViewModel.School = _mapper.Map<SchoolModel>(await _schoolRepository.GetByIDAsync(arguments[0]));
            return activityViewModel;
        }

        public async Task<ActivityVM> InitiateEdit(params object[] arguments)
        {
            ActivityVM activityViewModel = new ActivityVM();
            activityViewModel.Activity = _mapper.Map<Models.Models.ActivityModel>(await _unitOfWork.ActivityRepository.GetByIDAsync(arguments[0]));
            activityViewModel.School = _mapper.Map<SchoolModel>(await _unitOfWork.SchoolRepository.GetByIDAsync(arguments[1]));
            return activityViewModel;
        }
    }
}
