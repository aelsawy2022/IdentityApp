using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using System;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class UserTypesController : Controller
    {
        private readonly IUserTypeService _userTypeService;

        public UserTypesController(IUserTypeService userTypeService)
        {
            _userTypeService = userTypeService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userTypeService.GetAllUserTypes());
        }

        public async Task<IActionResult> Create()
        {
            return View(await _userTypeService.InitiateCreate());
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
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
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
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

        public async Task<IActionResult> Edit(Guid userTypeId)
        {
            return View(await _userTypeService.InitiateEdit(userTypeId));
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
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

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
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }
    }
}
