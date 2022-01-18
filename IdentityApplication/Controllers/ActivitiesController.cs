using IdentityApplication.Data.Entities;
using IdentityApplication.Data.UnitOfWorks;
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

        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.ActivityRepository.GetAllAsync(o => o.OrderBy(a => a.CreationDate), "School") as List<Activity>);
        }

        public async Task<IActionResult> Create()
        {
            ActivityViewModel activityViewModel = new ActivityViewModel();
            activityViewModel.Activity = new Activity();
            activityViewModel.Schools = await _unitOfWork.SchoolRepository.GetAllAsync() as List<School>;
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
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ViewBag.Error = ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }

        [Authorize(Policy = "RequireSuperAdmin")]
        public async Task<IActionResult> ActivateActivity(Guid activityId)
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
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ViewBag.Error = ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(Guid activityId)
        {
            ActivityViewModel activityViewModel = new ActivityViewModel();
            activityViewModel.Activity = await _unitOfWork.ActivityRepository.GetByIDAsync(activityId);
            activityViewModel.Schools = await _unitOfWork.SchoolRepository.GetAllAsync() as List<School>;
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
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ViewBag.Error = ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid activityId)
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
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ViewBag.Error = ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }
    }
}
