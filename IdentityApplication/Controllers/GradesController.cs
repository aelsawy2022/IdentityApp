using IdentityApplication.Data.Entities;
using IdentityApplication.Data.UnitOfWorks;
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

        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.GradeRepository.GetAllAsync(o => o.OrderBy(g => g.Name), "School") as List<Grade>);
        }

        public async Task<IActionResult> Create()
        {
            GradeViewModel gradeViewModel = new GradeViewModel();
            gradeViewModel.Grade = new Grade();
            gradeViewModel.Schools = await _unitOfWork.SchoolRepository.GetAllAsync() as List<School>;
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
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ViewBag.Error = ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid gradeId)
        {
            GradeViewModel gradeViewModel = new GradeViewModel();
            gradeViewModel.Grade = await _unitOfWork.GradeRepository.GetByIDAsync(gradeId);
            gradeViewModel.Schools = await _unitOfWork.SchoolRepository.GetAllAsync() as List<School>;
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
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ViewBag.Error = ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid gradeId)
        {
            try
            {
                Grade grade = await _unitOfWork.GradeRepository.GetByIDAsync(gradeId);
                if (grade == null)
                {
                    ViewBag.Error = "Not Found";
                    return View("Index");
                }

                await _unitOfWork.GradeRepository.DeleteAsync(grade);
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
