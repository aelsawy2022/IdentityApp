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
    }
}
