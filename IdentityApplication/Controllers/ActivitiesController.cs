using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using System;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IActivityService _activityService;

        public ActivitiesController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        public async Task<IActionResult> Index(Guid schoolId)
        {
            if (schoolId == null || schoolId == Guid.Empty) return RedirectToAction("Index", "Schools");

            return View(await _activityService.Initiate(schoolId));
        }

        public async Task<IActionResult> Create(Guid schoolId)
        {
            return View(await _activityService.InitiateCreate(schoolId));
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
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

        [Authorize(Policy = "RequireSuperAdmin")]
        public async Task<IActionResult> ActivateActivity(Guid activityId, Guid schoolId)
        {
            try
            {
                bool succeded = await _activityService.ActivateActivity(activityId);

                if(!succeded) TempData["ErrorMsg"] = "Something wrong";

                return RedirectToAction("Index", new { schoolId = schoolId });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

        public async Task<IActionResult> Edit(Guid activityId, Guid schoolId)
        {
            return View(await _activityService.InitiateEdit(activityId, schoolId));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ActivityModel activity)
        {
            try
            {
                bool succeded = await _activityService.Edit(activity);

                if(!succeded ) TempData["ErrorMsg"] = "Something wrong";

                return RedirectToAction("Index", new { schoolId = activity.School.Id });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

        public async Task<IActionResult> Delete(Guid activityId, Guid schoolId)
        {
            try
            {
                bool succeded = await _activityService.Delete(activityId);
                
                if(!succeded) TempData["ErrorMsg"] = "Something wrong";

                return RedirectToAction("Index", new { schoolId = schoolId });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }
    }
}
