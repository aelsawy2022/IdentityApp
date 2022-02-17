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
using SchoolManagement.Persistance.Repositories.GenericRepo;

namespace SchoolManagement.Core.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IRepository<ActivityClass> _activityClassRepository;
        private readonly IRepository<ActivityUserType> _activityUserTypeRepository;
        private readonly IRepository<ActivitySlot> _activitySlotRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActivityService(
            IActivityRepository activityRepository,
            ISchoolRepository schoolRepository,
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
, IRepository<ActivityClass> activityClassRepository, IRepository<ActivityUserType> activityUserTypeRepository, IRepository<ActivitySlot> activitySlotRepository)
        {
            _activityRepository = activityRepository;
            _schoolRepository = schoolRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _activityClassRepository = activityClassRepository;
            _activityUserTypeRepository = activityUserTypeRepository;
            _activitySlotRepository = activitySlotRepository;
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

        public async Task<bool> AddSlots(ActivityModel activity, List<ActivitySlotModel> activitySlots)
        {
            foreach (var slot in activitySlots)
            {
                await AddSlots(activity, slot);
            }
            return true;
        }

        public async Task<bool> AddSlots(ActivityModel activity, ActivitySlotModel activitySlot)
        {
            if (activity == null) return false;
            if (activitySlot == null) return false;

            Activity activityEntity = await _activityRepository.GetByIDAsync(activity.Id);

            if (activityEntity.Slots == null) activityEntity.Slots = new List<ActivitySlot>();
            activityEntity.Slots.Add(_mapper.Map<ActivitySlot>(activitySlot));

            await _activityRepository.UpdateAsync(activityEntity);
            await _activityRepository.SaveAsync();

            return true;
        }

        public async Task<bool> Configure(ActivityVM activityVM)
        {
            if (activityVM == null || activityVM.Activity == null) return false;

            Persistance.Data.Entities.Activity activity = _unitOfWork.ActivityRepository.GetByID(activityVM.Activity.Id);

            if (activityVM.UserTypes != null)
            {
                foreach (UserTypeModel userType in activityVM.UserTypes)
                {
                    ActivityUserType activityUserType = await _activityUserTypeRepository.GetOneAsync(aut => aut.UserTypeId == userType.Id && aut.ActivityId == activity.Id);

                    if (userType.IsSelected && activityUserType == null)
                    {
                        await _activityUserTypeRepository.AddAsync(new ActivityUserType()
                        {
                            UserType = await _unitOfWork.UserTypeRepository.GetByIDAsync(userType.Id),
                            UserTypeId = userType.Id,
                            Activity = activity,
                            ActivityId = activity.Id
                        });
                    }
                    else if(!userType.IsSelected && activityUserType != null)
                    {
                        await _activityUserTypeRepository.DeleteAsync(activityUserType);
                    }
                }
                await _activityUserTypeRepository.SaveAsync();
            }

            if (activityVM.Grades != null)
            {
                foreach (GradeModel grade in activityVM.Grades)
                {
                    foreach (ClassModel _class in grade.Classes)
                    {
                        ActivityClass activityClass = await _activityClassRepository.GetOneAsync(ac => ac.ClassId == _class.Id && ac.ActivityId == activity.Id);

                        if (_class.IsSelected && activityClass == null)
                        {
                            await _activityClassRepository.AddAsync(new ActivityClass()
                            {
                                Class = await _unitOfWork.ClassRepository.GetByIDAsync(_class.Id),
                                ClassId = _class.Id,
                                Activity = activity,
                                ActivityId = activity.Id
                            });
                        }
                        else if (!_class.IsSelected && activityClass != null)
                        {
                            await _activityClassRepository.DeleteAsync(activityClass);
                        }
                    }
                }
                await _activityClassRepository.SaveAsync();
            }

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

        public async Task<bool> DeleteSlot(Guid slotId)
        {
            if (slotId == Guid.Empty) return false;

            ActivitySlot slot = await _activitySlotRepository.GetByIDAsync(slotId);

            if (slot == null) return false;

            await _activitySlotRepository.DeleteAsync(slot);
            await _activitySlotRepository.SaveAsync();

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

        public async Task<List<ClassModel>> GetActivityClasses(Guid activityId)
        {
            List<Class> activityClasses = (await _activityClassRepository.GetAsync(a => a.ActivityId == activityId, null, "Class") as List<ActivityClass>).Select(s => s.Class).ToList();
            return _mapper.Map<List<ClassModel>>(activityClasses);
        }

        public async Task<List<UserTypeModel>> GetActivityUserTypes(Guid activityId)
        {
            List<UserType> userTypes = (await _activityUserTypeRepository.GetAsync(a => a.ActivityId == activityId, null, "UserType") as List<ActivityUserType>).Select(s => s.UserType).ToList();
            return _mapper.Map<List<UserTypeModel>>(userTypes);
        }

        public async Task<ActivityModel> GetById(Guid id)
        {
            return _mapper.Map<ActivityModel>(await _unitOfWork.ActivityRepository.GetByIDAsync(id));
        }

        public async Task<ActivityModel> GetWithSlotsById(Guid id)
        {
            if (id == Guid.Empty) return null;

            Activity activity = await _activityRepository.GetOneAsync(a => a.Id == id, "Slots");
            return _mapper.Map<ActivityModel>(activity);
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
        /*
         activityViewModel.ActivitySlots = new List<ActivitySlotModel>();
            activityViewModel.UserTypes = _mapper.Map<List<UserTypeModel>>(await _unitOfWork.UserTypeRepository.GetAllAsync());
            var grades = await _unitOfWork.GradeRepository.GetAsync(g => g.School.Id == activityViewModel.School.Id, o => o.OrderBy(g => g.CreationDate), "Classes") as List<Grade>;
            activityViewModel.Grades = _mapper.Map<List<GradeModel>>(grades);
         */

        public async Task<ActivityVM> InitiateEdit(params object[] arguments)
        {
            ActivityVM activityViewModel = new ActivityVM();
            activityViewModel.Activity = _mapper.Map<Models.Models.ActivityModel>(await _unitOfWork.ActivityRepository.GetByIDAsync(arguments[0]));
            activityViewModel.School = _mapper.Map<SchoolModel>(await _unitOfWork.SchoolRepository.GetByIDAsync(arguments[1]));
            return activityViewModel;
        }
    }
}
