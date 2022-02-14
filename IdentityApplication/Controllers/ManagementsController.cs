using IdentityApplication.Bases;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Infrastructure.CustomFilters;
using SchoolManagement.Models.Models;
using System;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class ManagementsController : BaseController
    {
        private readonly IManagementService _managementService;

        public ManagementsController(
            IManagementService managementService
            )
        {
            _managementService = managementService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _managementService.Initiate());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize(Roles.SUPER_ADMIN)]
        public async Task<IActionResult> Create()
        {
            try
            {
                return View(await _managementService.InitiateCreate());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ManagementModel management)
        {
            try
            {
                bool succeded = await _managementService.Create(management);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> Edit(Guid managementId)
        {
            try
            {
                return View(await _managementService.InitiateEdit(managementId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ManagementModel management)
        {
            try
            {
                bool succeded = await _managementService.Edit(management);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize(Roles.SUPER_ADMIN)]
        public async Task<IActionResult> Delete(Guid managementId)
        {
            try
            {
                bool succeded = await _managementService.Delete(managementId);
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
