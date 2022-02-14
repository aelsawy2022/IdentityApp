using IdentityApplication.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using System;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class UserTypesController : BaseController
    {
        private readonly IUserTypeService _userTypeService;

        public UserTypesController(IUserTypeService userTypeService)
        {
            _userTypeService = userTypeService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _userTypeService.GetAllUserTypes());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [SchoolManagement.Infrastructure.CustomFilters.Authorize(SchoolManagement.Infrastructure.CustomFilters.Roles.SUPER_ADMIN)]
        public async Task<IActionResult> Create()
        {
            try
            {
                return View(await _userTypeService.InitiateCreate());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserTypeModel userType)
        {
            try
            {
                bool succeded = await _userTypeService.Create(userType);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [Authorize(Policy = "RequireSuperAdmin")]
        public async Task<IActionResult> ActivateType(Guid typeId)
        {
            try
            {
                bool succeded = await _userTypeService.ActivateType(typeId);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> Edit(Guid userTypeId)
        {
            try
            {
                return View(await _userTypeService.InitiateEdit(userTypeId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserTypeModel userType)
        {
            try
            {
                bool succeded = await _userTypeService.Edit(userType);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [SchoolManagement.Infrastructure.CustomFilters.Authorize(SchoolManagement.Infrastructure.CustomFilters.Roles.SUPER_ADMIN)]
        public async Task<IActionResult> Delete(Guid userTypeId)
        {
            try
            {
                bool succeded = await _userTypeService.Delete(userTypeId);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
