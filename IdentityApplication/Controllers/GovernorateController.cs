using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using System;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class GovernorateController : Controller
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
            return View(await _governorateService.GetAllGovernorates());
        }

        public async Task<IActionResult> Create()
        {
            return View(await _governorateService.InitiateCreate());
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
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

        public async Task<IActionResult> Edit(Guid governorateId)
        {
            return View(await _governorateService.InitiateEdit(governorateId));
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
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

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
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }
    }
}
