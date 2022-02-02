using IdentityApplication.Bases;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using System;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class GradesController : BaseController
    {
        private readonly IGradesService _gradesService;

        public GradesController(IGradesService gradesService)
        {
            _gradesService = gradesService;
        }

        public async Task<IActionResult> Index(Guid schoolId)
        {
            try
            {
                if (schoolId == null || schoolId == Guid.Empty) return RedirectToAction("Index", "Schools");

                return View(await _gradesService.Initiate(schoolId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> Create(Guid schoolId)
        {
            try
            {
                return View(await _gradesService.InitiateCreate(schoolId));
            }
            catch (Exception ex)
            {
                throw;
            }
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
                throw;
            }
        }

        public async Task<IActionResult> Edit(Guid gradeId, Guid schoolId)
        {
            try
            {
                return View(await _gradesService.InitiateEdit(gradeId, schoolId));
            }
            catch (Exception ex)
            {
                throw;
            }
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
                throw;
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
                throw;
            }
        }
    }
}
