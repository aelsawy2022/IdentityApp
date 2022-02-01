using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using System;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class GradesController : Controller
    {
        private readonly IGradesService _gradesService;

        public GradesController(IGradesService gradesService)
        {
            _gradesService = gradesService;
        }

        public async Task<IActionResult> Index(Guid schoolId)
        {
            if (schoolId == null || schoolId == Guid.Empty) return RedirectToAction("Index", "Schools");

            return View(await _gradesService.Initiate(schoolId));
        }

        public async Task<IActionResult> Create(Guid schoolId)
        {
            return View(await _gradesService.InitiateCreate(schoolId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(GradeModel grade)
        {
            try
            {
                bool succeded = await _gradesService.Create(grade);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index", new { schoolId = grade.School.Id });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

        public async Task<IActionResult> Edit(Guid gradeId, Guid schoolId)
        {
            return View(await _gradesService.InitiateEdit(gradeId, schoolId));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GradeModel grade)
        {
            try
            {
                bool succeded = await _gradesService.Edit(grade);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index", new { schoolId = grade.School.Id });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

        public async Task<IActionResult> Delete(Guid gradeId, Guid schoolId)
        {
            try
            {
                bool succeded = await _gradesService.Delete(gradeId);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";

                return View("Index", new { schoolId = schoolId });
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
