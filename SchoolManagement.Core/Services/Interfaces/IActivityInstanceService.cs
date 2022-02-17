using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface IActivityInstanceService : IBaseService<ActivityInstanceModel, ActivityInstanceModel>
    {
        Task<List<ActivityInstanceModel>> GetAllActivityInstances(Guid activityId);
        Task<ActivityInstanceModel> GetById(Guid instanceId);
    }
}
