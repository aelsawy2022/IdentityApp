using IdentityApplication.Bases;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Infrastructure.CustomFilters;
using SchoolManagement.Models.Models;
using SchoolManagement.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class ActivitiesController : BaseController
    {
        private readonly IActivityService _activityService;
        private readonly IUserTypeService _userTypeService;
        private readonly IGradesService _gradesService;
        private readonly ISchoolService _schoolService;

        public ActivitiesController(
            IActivityService activityService,
            IUserTypeService userTypeService, IGradesService gradesService, ISchoolService schoolService)
        {
            _activityService = activityService;
            _userTypeService = userTypeService;
            _gradesService = gradesService;
            _schoolService = schoolService;
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

        [Authorize(Roles.ADMIN)]
        [HttpPost]
        public IActionResult AddSlot(ActivityVM activityVM)
        {
            try
            {
                if (activityVM.ActivitySlots == null) activityVM.ActivitySlots = new List<ActivitySlotModel>();
                activityVM.ActivitySlots.Add(activityVM.Slot);
                activityVM.Slot = new ActivitySlotModel();

                return PartialView("_Slots", activityVM.ActivitySlots);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityVM activityVM)
        {
            try
            {
                bool succeded = await _activityService.Create(activityVM.Activity);

                if (!succeded) TempData["ErrorMsg"] = "Something wrong";

                return RedirectToAction("Index", new { schoolId = activityVM.Activity.School.Id });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize(Roles.SCHOOL_ACTIVITY)]
        public async Task<IActionResult> Configure(Guid activityId, Guid schoolId)
        {
            ActivityVM activityVM = new ActivityVM();
            activityVM.UserTypes = await _userTypeService.GetAllUserTypes();
            activityVM.Grades = await _gradesService.GetGradsWithClassesBySchoolId(schoolId);
            activityVM.Activity = await _activityService.GetById(activityId);
            activityVM.School = await _schoolService.GetById(schoolId);

            var activityClasses = await _activityService.GetActivityClasses(activityId);
            foreach (var activityClass in activityClasses)
            {
                foreach(var grade in activityVM.Grades)
                {
                    var _class = grade.Classes.FirstOrDefault(c => c.Id == activityClass.Id);
                    if (_class != null) _class.IsSelected = true;
                }
            }
            var activityUserTypes = await _activityService.GetActivityUserTypes(activityId);
            foreach (var activityUserType in activityUserTypes)
            {
                var userType = activityVM.UserTypes.FirstOrDefault(ut => ut.Id == activityUserType.Id);
                if (userType != null) userType.IsSelected = true;
            }

            return View(activityVM);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(ActivityVM activityVM)
        {
            try
            {
                bool succeded = await _activityService.Configure(activityVM);
                return RedirectToAction("Index", new { schoolId = activityVM.School.Id });
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [Authorize(Roles.SCHOOL_ACTIVITY)]
        public async Task<IActionResult> Slots(Guid activityId, Guid schoolId)
        {
            ActivityVM activityVM = new ActivityVM();
            activityVM.Activity = await _activityService.GetWithSlotsById(activityId);
            activityVM.School = await _schoolService.GetById(schoolId);
            return View(activityVM);
        }

        [HttpPost]
        public async Task<IActionResult> Slots(ActivityVM activityVM)
        {
            bool succeded = await _activityService.AddSlots(activityVM.Activity, activityVM.Slot);
            return RedirectToAction("Slots", new { activityId = activityVM.Activity.Id, schoolId = activityVM.School.Id});
        }

        [Authorize(Roles.SCHOOL_ACTIVITY)]
        public async Task<IActionResult> DeleteSlot(Guid activityId, Guid schoolId, Guid slotId)
        {
            bool succeded = await _activityService.DeleteSlot(slotId);
            return RedirectToAction("Slots", new { activityId = activityId, schoolId = schoolId });
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
