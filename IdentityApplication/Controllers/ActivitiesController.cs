using IdentityApplication.Data.Entities;
using IdentityApplication.Data.UnitOfWorks;
using IdentityApplication.Models;
using IdentityApplication.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActivitiesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(Guid schoolId)
        {
            if(schoolId == null || schoolId == Guid.Empty) return RedirectToAction("Index", "Schools");

            ActivityViewModel activityViewModel = new ActivityViewModel();
            activityViewModel.Activities = await _unitOfWork.ActivityRepository.GetAsync(a => a.School.Id == schoolId, o => o.OrderBy(a => a.CreationDate)) as List<Activity>;
            activityViewModel.School = await _unitOfWork.SchoolRepository.GetByIDAsync(schoolId);
            return View(activityViewModel);
        }

        public async Task<IActionResult> Create(Guid schoolId)
        {
            ActivityViewModel activityViewModel = new ActivityViewModel();
            activityViewModel.Activity = new Activity();
            activityViewModel.School = await _unitOfWork.SchoolRepository.GetByIDAsync(schoolId);
            return View(activityViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Activity activity)
        {
            try
            {
                activity.CreationDate = DateTime.Now;
                activity.Id = Guid.NewGuid();
                activity.School = await _unitOfWork.SchoolRepository.GetByIDAsync(activity.School.Id);

                await _unitOfWork.ActivityRepository.AddAsync(activity);
                await _unitOfWork.SaveAsync();
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

                Activity activity = await _unitOfWork.ActivityRepository.GetByIDAsync(activityId);
                if (activity == null)
                {
                    ViewBag.Error = "Type not found";
                    return View("Index");
                }

                activity.Active = !activity.Active;
                await _unitOfWork.ActivityRepository.UpdateAsync(activity);
                await _unitOfWork.SaveAsync();
                return RedirectToAction("Index", new { schoolId = schoolId});
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
            ActivityViewModel activityViewModel = new ActivityViewModel();
            activityViewModel.Activity = await _unitOfWork.ActivityRepository.GetByIDAsync(activityId);
            activityViewModel.School = await _unitOfWork.SchoolRepository.GetByIDAsync(schoolId);
            return View(activityViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Activity activity)
        {
            try
            {
                activity.School = await _unitOfWork.SchoolRepository.GetByIDAsync(activity.School.Id);

                await _unitOfWork.ActivityRepository.UpdateAsync(activity);
                await _unitOfWork.SaveAsync();
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
                Activity activity = await _unitOfWork.ActivityRepository.GetByIDAsync(activityId);
                if (activity == null)
                {
                    ViewBag.Error = "Not Found";
                    return View("Index");
                }

                await _unitOfWork.ActivityRepository.DeleteAsync(activity);
                await _unitOfWork.SaveAsync();
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
