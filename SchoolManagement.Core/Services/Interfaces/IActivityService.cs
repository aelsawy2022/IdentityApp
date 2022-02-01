using SchoolManagement.Models.Models;
using SchoolManagement.Models.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface IActivityService : IBaseService<ActivityModel, ActivityViewModel>
    {
        Task<bool> ActivateActivity(params object[] arguments);
    }
}
