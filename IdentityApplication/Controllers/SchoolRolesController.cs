using AutoMapper;
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
    public class SchoolRolesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SchoolRolesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(Guid schoolId)
        {
            try
            {
                RoleViewModel roleViewModel = new RoleViewModel();

                roleViewModel.Roles = await _unitOfWork.RoleRepository.GetAsync(r => r.School.Id == schoolId) as List<Role>;
                roleViewModel.School = await _unitOfWork.SchoolRepository.GetByIDAsync(schoolId);

                return View(roleViewModel);
            }
            catch(Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

        public async Task<IActionResult> Create(Guid schoolId)
        {
            RoleViewModel roleViewModel = new RoleViewModel();
            roleViewModel.Role = new Role();
            roleViewModel.School = await _unitOfWork.SchoolRepository.GetByIDAsync(schoolId);
            return View(roleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Role role)
        {
            try
            {
                role.Id = Guid.NewGuid();
                role.Name = role.Name.Trim().Replace(" ", "");
                role.School = await _unitOfWork.SchoolRepository.GetByIDAsync(role.School.Id);

                await _unitOfWork.RoleRepository.AddAsync(role);
                await _unitOfWork.SaveAsync();

                return RedirectToAction("Index", new { schoolId = role.School.Id });
            }
            catch(Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }


        public async Task<IActionResult> Edit(string roleId, Guid schoolId)
        {
            RoleViewModel roleViewModel = new RoleViewModel();
            roleViewModel.Role = await _unitOfWork.RoleRepository.GetByIDAsync(roleId);
            roleViewModel.School = await _unitOfWork.SchoolRepository.GetByIDAsync(schoolId);
            return View(roleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Role role)
        {
            try
            {
                Role _role = await _unitOfWork.RoleRepository.GetByIDAsync(role.Id);

                _role.Name = role.Name.Trim().Replace(" ", "");
                _role.Active = role.Active;

                _role.School = await _unitOfWork.SchoolRepository.GetByIDAsync(role.School.Id);

                await _unitOfWork.RoleRepository.UpdateAsync(_role);
                await _unitOfWork.SaveAsync();

                return RedirectToAction("Index", new { schoolId = role.School.Id });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

        public async Task<IActionResult> Delete(string roleId, Guid schoolId)
        {
            try
            {
                Role role = await _unitOfWork.RoleRepository.GetByIDAsync(roleId);

                if (role == null)
                {
                    ViewBag.Error = "Not Found";
                    return View("Index", new { schoolId = schoolId });
                }

                await _unitOfWork.RoleRepository.DeleteAsync(role);
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

        public async Task<IActionResult> ActivateRole(string roleId, Guid schoolId)
        {
            try
            {
                Role role = await _unitOfWork.RoleRepository.GetByIDAsync(roleId);
                if (role == null)
                {
                    ViewBag.Error = "Type not found";
                    return View("Index");
                }
                role.Active = !role.Active;

                await _unitOfWork.RoleRepository.UpdateAsync(role);
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
