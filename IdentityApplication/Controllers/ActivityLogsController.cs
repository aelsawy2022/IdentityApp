using IdentityApplication.Bases;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Infrastructure.CustomFilters;
using SchoolManagement.Models.Models;
using SchoolManagement.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(CustomeExceptionFilter))]
    public class ActivityLogsController : Controller
    {
        private readonly IActivityLogService _activityLogService;

        public ActivityLogsController(IActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
        }

        [Authorize(Roles.SUPER_ADMIN)]
        public async Task<IActionResult> Index(
            string username = null,
            DateTime? created = null,
            int currentPage = 1, int maxRows = 10,
            string searchBtn = null, string clearBtn = null
            )
        {
            ActivityLogVM activityLogVM = new ActivityLogVM();
            activityLogVM.ActivityLogFilter = new ActivityLogFilter();
            activityLogVM.ActivityLogFilter.Username = username;
            activityLogVM.ActivityLogFilter.CreationDate = created;
            activityLogVM.ActivityLogFilter.CurrentPage = currentPage;
            if(searchBtn != null && searchBtn == "Search")
            {
                activityLogVM.ActivityLogFilter.CurrentPage = 1;
            }
            activityLogVM.ActivityLogFilter.MaxRows = maxRows;

            activityLogVM.ActivityLogs = await _activityLogService.GetLogs(activityLogVM.ActivityLogFilter);
            return View(activityLogVM);
        }
    }
}
