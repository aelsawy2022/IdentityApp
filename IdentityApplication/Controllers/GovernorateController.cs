using IdentityApplication.Bases;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Infrastructure.CustomFilters;
using SchoolManagement.Models.Models;
using System;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class GovernorateController : BaseController
    {
        private readonly IGovernorateService _governorateService;
        public GovernorateController(
            IGovernorateService governorateService
            )
        {
            _governorateService = governorateService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _governorateService.GetAllGovernorates());
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
                return View(await _governorateService.InitiateCreate());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(GovernorateModel governorate)
        {
            try
            {
                bool succeded = await _governorateService.Create(governorate);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> Edit(Guid governorateId)
        {
            try
            {
                return View(await _governorateService.InitiateEdit(governorateId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GovernorateModel governorate)
        {
            try
            {
                bool succeded = await _governorateService.Edit(governorate);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize(Roles.SUPER_ADMIN)]
        public async Task<IActionResult> Delete(Guid governorateId)
        {
            try
            {
                bool succeded = await _governorateService.Delete(governorateId);
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
