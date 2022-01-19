using IdentityApplication.Data.Entities;
using IdentityApplication.Data.UnitOfWorks;
using IdentityApplication.Models;
using IdentityApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class ClassesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(Guid gradeId, Guid schoolId)
        {
            if (gradeId == null || gradeId == Guid.Empty) return RedirectToAction("Index", "Grades", new { schoolId = schoolId });

            ClassViewModel classViewModel = new ClassViewModel();
            classViewModel.Classes = await _unitOfWork.ClassRepository.GetAsync(c => c.Grade.Id == gradeId, o => o.OrderBy(c => c.CreationDate)) as List<Class>;
            classViewModel.Grade = await _unitOfWork.GradeRepository.GetByIDAsync(gradeId);
            classViewModel.SchoolId = schoolId;
            return View(classViewModel);
        }

        public async Task<IActionResult> Create(Guid gradeId, Guid schoolId)
        {
            ClassViewModel classViewModel = new ClassViewModel();
            classViewModel.Class = new Class();
            classViewModel.Grade = await _unitOfWork.GradeRepository.GetByIDAsync(gradeId);
            classViewModel.SchoolId = schoolId;
            return View(classViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Class Class)
        {
            try
            {
                Guid schoolId = Class.Grade.School.Id;

                Class.CreationDate = DateTime.Now;
                Class.Id = Guid.NewGuid();
                Class.Grade = await _unitOfWork.GradeRepository.GetByIDAsync(Class.Grade.Id);

                await _unitOfWork.ClassRepository.AddAsync(Class);
                await _unitOfWork.SaveAsync();
                return RedirectToAction("Index", new { gradeId = Class.Grade.Id, schoolId = schoolId });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

        public async Task<IActionResult> Edit(Guid classId, Guid gradeId, Guid schoolId)
        {
            ClassViewModel classViewModel = new ClassViewModel();
            classViewModel.Class = await _unitOfWork.ClassRepository.GetByIDAsync(classId);
            classViewModel.Grade = await _unitOfWork.GradeRepository.GetByIDAsync(gradeId);
            classViewModel.SchoolId = schoolId;
            return View(classViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Class Class)
        {
            try
            {
                Guid schoolId = Class.Grade.School.Id;

                Class.Grade = await _unitOfWork.GradeRepository.GetByIDAsync(Class.Grade.Id);

                await _unitOfWork.ClassRepository.UpdateAsync(Class);
                await _unitOfWork.SaveAsync();

                return RedirectToAction("Index", new { gradeId = Class.Grade.Id, schoolId = schoolId });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

        public async Task<IActionResult> Delete(Guid classId, Guid gradeId, Guid schoolId)
        {
            try
            {
                Class _class = await _unitOfWork.ClassRepository.GetByIDAsync(classId);

                if (_class == null) ViewBag.Error = "Not Found";

                await _unitOfWork.ClassRepository.DeleteAsync(_class);
                await _unitOfWork.SaveAsync();

                return RedirectToAction("Index", new { gradeId = gradeId, schoolId = schoolId });
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
