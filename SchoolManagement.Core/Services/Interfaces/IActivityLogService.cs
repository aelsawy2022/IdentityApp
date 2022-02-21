using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface IActivityLogService
    {
        void Log(ActivityLogModel activityLog);
        Task RemoveUsersActivitiesThatHasBeenOveraMonth();
    }
}
