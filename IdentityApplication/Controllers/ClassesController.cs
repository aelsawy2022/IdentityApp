using IdentityApplication.Bases;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class ClassesController : BaseController
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        public async Task<IActionResult> Index(Guid gradeId, Guid schoolId)
        {
            try
            {
                if (gradeId == null || gradeId == Guid.Empty) return RedirectToAction("Index", "Grades", new { schoolId = schoolId });

                return View(await _classService.Initiate(gradeId, schoolId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> Create(Guid gradeId, Guid schoolId)
        {
            try
            {
                return View(await _classService.InitiateCreate(gradeId, schoolId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClassModel Class)
        {
            try
            {
                Guid schoolId = Class.Grade.School.Id;
                bool succeded = await _classService.Create(Class);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index", new { gradeId = Class.Grade.Id, schoolId = schoolId });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> Edit(Guid classId, Guid gradeId, Guid schoolId)
        {
            try
            {
                return View(await _classService.InitiateEdit(classId, gradeId, schoolId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClassModel _class)
        {
            try
            {
                Guid schoolId = _class.Grade.School.Id;
                bool succeded = await _classService.Edit(_class);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index", new { gradeId = _class.Grade.Id, schoolId = schoolId });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> Delete(Guid classId, Guid gradeId, Guid schoolId)
        {
            try
            {

                bool succeded = await _classService.Delete(classId);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index", new { gradeId = gradeId, schoolId = schoolId });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
