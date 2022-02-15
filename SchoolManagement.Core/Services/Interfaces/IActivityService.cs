using SchoolManagement.Models.Models;
using SchoolManagement.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface IActivityService : IBaseService<ActivityModel, ActivityVM>
    {
        Task<bool> ActivateActivity(params object[] arguments);
        Task<ActivityModel> GetById(Guid id);
        Task<List<ClassModel>> GetActivityClasses(Guid activityId);
        Task<List<UserTypeModel>> GetActivityUserTypes(Guid activityId);
        Task<ActivityModel> GetWithSlotsById(Guid id);
        Task<bool> Configure(ActivityVM activityVM);
        Task<bool> AddSlots(ActivityModel activity, List<ActivitySlotModel> activitySlots);
        Task<bool> AddSlots(ActivityModel activity, ActivitySlotModel activitySlot);
        Task<bool> DeleteSlot(Guid slotId);
    }
}
