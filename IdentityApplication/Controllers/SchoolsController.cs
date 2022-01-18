using IdentityApplication.Data.Entities;
using IdentityApplication.Data.Repositories.ManagementRepo;
using IdentityApplication.Data.Repositories.SchoolRepo;
using IdentityApplication.Data.UnitOfWorks;
using IdentityApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class SchoolsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SchoolsController(
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var schools = await _unitOfWork.SchoolRepository.GetAllAsync(o => o.OrderBy(s => s.Name), "Address,Management") as List<School>;
            return View(schools);
        }

        public async Task<IActionResult> Create()
        {
            SchoolViewModel schoolViewModel = new SchoolViewModel();
            schoolViewModel.School = new School();
            schoolViewModel.Managements = await _unitOfWork.ManagementRepository.GetAllAsync() as List<Management>;
            return View(schoolViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(School school)
        {
            try
            {
                school.CreationDate = DateTime.Now;
                school.Id = Guid.NewGuid();
                Management management = await _unitOfWork.ManagementRepository.GetByIDAsync(school.Management.Id);
                school.Management = management;
                school.Address.CreationDate = school.CreationDate;
                school.Address.Id = Guid.NewGuid();

                await _unitOfWork.AddressRepository.AddAsync(school.Address);
                await _unitOfWork.SchoolRepository.AddAsync(school);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ViewBag.Error = ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid schoolId)
        {
            SchoolViewModel schoolViewModel = new SchoolViewModel();
            schoolViewModel.School = await _unitOfWork.SchoolRepository.GetOneAsync(e => e.Id == schoolId, "Address");
            schoolViewModel.Managements = await _unitOfWork.ManagementRepository.GetAllAsync() as List<Management>;
            return View(schoolViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(School school)
        {
            try
            {
                school.Management = await _unitOfWork.ManagementRepository.GetByIDAsync(school.Management.Id);

                await _unitOfWork.AddressRepository.UpdateAsync(school.Address);
                await _unitOfWork.SchoolRepository.UpdateAsync(school);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ViewBag.Error = ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid schoolId)
        {
            try
            {
                School school = await _unitOfWork.SchoolRepository.GetByIDAsync(schoolId);
                if (school == null)
                {
                    ViewBag.Error = "Not Found";
                    return View("Index");
                }

                await _unitOfWork.SchoolRepository.DeleteAsync(school);
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
