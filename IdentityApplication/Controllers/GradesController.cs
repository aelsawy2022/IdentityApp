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
    public class GradesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GradesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(Guid schoolId)
        {
            if (schoolId == null || schoolId == Guid.Empty) return RedirectToAction("Index", "Schools");

            GradeViewModel gradeViewModel = new GradeViewModel();
            gradeViewModel.Grades = await _unitOfWork.GradeRepository.GetAsync(x => x.School.Id == schoolId, o => o.OrderBy(g => g.Name)) as List<Grade>;
            gradeViewModel.School = await _unitOfWork.SchoolRepository.GetByIDAsync(schoolId);
            return View(gradeViewModel);
        }

        public async Task<IActionResult> Create(Guid schoolId)
        {
            GradeViewModel gradeViewModel = new GradeViewModel();
            gradeViewModel.Grade = new Grade();
            gradeViewModel.School = await _unitOfWork.SchoolRepository.GetByIDAsync(schoolId);
            return View(gradeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Grade grade)
        {
            try
            {
                grade.CreationDate = DateTime.Now;
                grade.Id = Guid.NewGuid();
                grade.School = await _unitOfWork.SchoolRepository.GetByIDAsync(grade.School.Id);

                await _unitOfWork.GradeRepository.AddAsync(grade);
                await _unitOfWork.SaveAsync();
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
            GradeViewModel gradeViewModel = new GradeViewModel();
            gradeViewModel.Grade = await _unitOfWork.GradeRepository.GetByIDAsync(gradeId);
            gradeViewModel.School = await _unitOfWork.SchoolRepository.GetByIDAsync(schoolId);
            return View(gradeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Grade grade)
        {
            try
            {
                grade.School = await _unitOfWork.SchoolRepository.GetByIDAsync(grade.School.Id);

                await _unitOfWork.GradeRepository.UpdateAsync(grade);
                await _unitOfWork.SaveAsync();
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
                Grade grade = await _unitOfWork.GradeRepository.GetByIDAsync(gradeId);

                if (grade == null)  ViewBag.Error = "Not Found";

                await _unitOfWork.GradeRepository.DeleteAsync(grade);
                await _unitOfWork.SaveAsync();

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
