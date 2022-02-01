using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using System;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class ManagementsController : Controller
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
            return View(await _managementService.Initiate());
        }

        public async Task<IActionResult> Create()
        {
            return View(await _managementService.InitiateCreate());
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
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

        public async Task<IActionResult> Edit(Guid managementId)
        {
            return View(await _managementService.InitiateEdit(managementId));
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
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

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
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }
    }
}
