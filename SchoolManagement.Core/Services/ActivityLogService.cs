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
    public class ActivityLogService : IActivityLogService
    {
        private readonly IRepository<ActivityLog> _activityLogRepository;
        private readonly IMapper _mapper;

        public ActivityLogService(IRepository<ActivityLog> activityLogRepository, IMapper mapper)
        {
            _activityLogRepository = activityLogRepository;
            _mapper = mapper;
        }

        public void Log(ActivityLogModel activityLog)
        {
            if (activityLog == null) return;

            _activityLogRepository.Add(_mapper.Map<ActivityLog>(activityLog));
            _activityLogRepository.Save();
        }

        public async Task RemoveUsersActivitiesThatHasBeenOveraMonth()
        {
            try
            {
                DateTime removedLogsDate = DateTime.Now.AddDays(-30);

                var logs = await _activityLogRepository.GetAsync(al => al.CreationDate.Date == removedLogsDate.Date) as List<ActivityLog>;

                if(logs != null && logs.Count > 0)
                {
                    foreach (ActivityLog log in logs)
                    {
                        await _activityLogRepository.DeleteAsync(log);
                    }
                    await _activityLogRepository.SaveAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
