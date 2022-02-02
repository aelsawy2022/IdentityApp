using IdentityApplication.Bases;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using System;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class SchoolRolesController : BaseController
    {
        private readonly ISchoolRoleService _schoolRoleService;

        public SchoolRolesController(ISchoolRoleService schoolRoleService)
        {
            _schoolRoleService = schoolRoleService;
        }

        public async Task<IActionResult> Index(Guid schoolId)
        {
            try
            {
                return View(await _schoolRoleService.Initiate(schoolId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> Create(Guid schoolId)
        {
            return View(await _schoolRoleService.InitiateCreate(schoolId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(RolesModel role)
        {
            try
            {
                bool succeded = await _schoolRoleService.Create(role);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";

                return RedirectToAction("Index", new { schoolId = role.School.Id });
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IActionResult> Edit(Guid roleId, Guid schoolId)
        {
            try
            {
                return View(await _schoolRoleService.InitiateEdit(roleId, schoolId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RolesModel role)
        {
            try
            {
                bool succeded = await _schoolRoleService.Edit(role);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";

                return RedirectToAction("Index", new { schoolId = role.School.Id });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> Delete(Guid roleId, Guid schoolId)
        {
            try
            {
                bool succeded = await _schoolRoleService.Delete(roleId);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";

                return RedirectToAction("Index", new { schoolId = schoolId });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> ActivateRole(Guid roleId, Guid schoolId)
        {
            try
            {
                bool succeded = await _schoolRoleService.ActivateRole(roleId);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";

                return RedirectToAction("Index", new { schoolId = schoolId });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
