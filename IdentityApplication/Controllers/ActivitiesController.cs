using IdentityApplication.Bases;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Infrastructure.CustomFilters;
using SchoolManagement.Models.Models;
using System;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class ActivitiesController : BaseController
    {
        private readonly IActivityService _activityService;

        public ActivitiesController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [Authorize(Roles.ADMIN, Roles.SCHOOL_ADMIN)]
        public async Task<IActionResult> Index(Guid schoolId)
        {
            try
            {
                if (schoolId == null || schoolId == Guid.Empty) return RedirectToAction("Index", "Schools");
                return View(await _activityService.Initiate(schoolId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize(Roles.ADMIN, Roles.SCHOOL_ADMIN)]
        public async Task<IActionResult> Create(Guid schoolId)
        {
            try
            {
                return View(await _activityService.InitiateCreate(schoolId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityModel activity)
        {
            try
            {
                bool succeded = await _activityService.Create(activity);

                if (!succeded) TempData["ErrorMsg"] = "Something wrong";

                return RedirectToAction("Index", new { schoolId = activity.School.Id });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize(Roles.SCHOOL_ACTIVITY)]
        public async Task<IActionResult> ActivateActivity(Guid activityId, Guid schoolId)
        {
            try
            {
                bool succeded = await _activityService.ActivateActivity(activityId);

                if (!succeded) TempData["ErrorMsg"] = "Something wrong";

                return RedirectToAction("Index", new { schoolId = schoolId });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize(Roles.SCHOOL_ACTIVITY)]
        public async Task<IActionResult> Edit(Guid activityId, Guid schoolId)
        {
            try
            {
                return View(await _activityService.InitiateEdit(activityId, schoolId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ActivityModel activity)
        {
            try
            {
                bool succeded = await _activityService.Edit(activity);

                if (!succeded) TempData["ErrorMsg"] = "Something wrong";

                return RedirectToAction("Index", new { schoolId = activity.School.Id });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize(Roles.SCHOOL_ACTIVITY)]
        public async Task<IActionResult> Delete(Guid activityId, Guid schoolId)
        {
            try
            {
                bool succeded = await _activityService.Delete(activityId);

                if (!succeded) TempData["ErrorMsg"] = "Something wrong";

                return RedirectToAction("Index", new { schoolId = schoolId });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
