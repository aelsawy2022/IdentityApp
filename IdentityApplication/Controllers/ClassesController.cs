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
    public class ClassesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.ClassRepository.GetAllAsync(o => o.OrderBy(c => c.CreationDate), "Grade") as List<Class>);
        }

        public async Task<IActionResult> Create()
        {
            ClassViewModel classViewModel = new ClassViewModel();
            classViewModel.Class = new Class();
            classViewModel.Grades = await _unitOfWork.GradeRepository.GetAllAsync() as List<Grade>;
            return View(classViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Class Class)
        {
            try
            {
                Class.CreationDate = DateTime.Now;
                Class.Id = Guid.NewGuid();
                Class.Grade = await _unitOfWork.GradeRepository.GetByIDAsync(Class.Grade.Id);

                await _unitOfWork.ClassRepository.AddAsync(Class);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ViewBag.Error = ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid classId)
        {
            ClassViewModel classViewModel = new ClassViewModel();
            classViewModel.Class = await _unitOfWork.ClassRepository.GetByIDAsync(classId);
            classViewModel.Grades = await _unitOfWork.GradeRepository.GetAllAsync() as List<Grade>;
            return View(classViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Class Class)
        {
            try
            {
                Class.Grade = await _unitOfWork.GradeRepository.GetByIDAsync(Class.Grade.Id);

                await _unitOfWork.ClassRepository.UpdateAsync(Class);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ViewBag.Error = ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid classId)
        {
            try
            {
                Class _class = await _unitOfWork.ClassRepository.GetByIDAsync(classId);
                if (_class == null)
                {
                    ViewBag.Error = "Not Found";
                    return View("Index");
                }

                await _unitOfWork.ClassRepository.DeleteAsync(_class);
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
