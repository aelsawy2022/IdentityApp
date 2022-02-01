using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.Models.Models.ViewModels;
using SchoolManagement.Persistance.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(
            IUserService userService
            )
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> List(int currentPage = 1, int maxRows = 2, string searchTxt = "")
        {
            return View(await _userService.GetUsers(currentPage, maxRows));
        }

        [Authorize(Policy = "RequireSuperAdmin")]
        public async Task<IActionResult> MakeAsAdmin(bool isAdmin, string userName, int currentPage = 1, int maxRows = 2)
        {
            bool succeded = await _userService.AddToAdminRole(userName, isAdmin);
            if (!succeded) TempData["ErrorMsg"] = "Something wrong";

            return RedirectToAction("List", new { currentPage = currentPage, maxRows = maxRows });
        }

        [Authorize(Policy = "RequireSuperAdmin")]
        public async Task<IActionResult> AddToSuperAdminRole(bool isSuperAdmin, string userName, int currentPage = 1, int maxRows = 2)
        {
            bool succeded = await _userService.AddToSuperAdminRole(userName, isSuperAdmin);
            if (!succeded) TempData["ErrorMsg"] = "Something wrong";
            return RedirectToAction("List", new { currentPage = currentPage, maxRows = maxRows });
        }

        public async Task<IActionResult> Edit(Guid userId)
        {
            return View(await _userService.InitiateEdit(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UsersViewModel usersViewModel)
        {
            try
            {
                bool succeded = await _userService.Edit(usersViewModel);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

        public async Task<IActionResult> Create()
        {
            return View(await _userService.InitiateCreate());
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsersViewModel usersViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserServiceResponse result = await _userService.CreateUser(usersViewModel);
                    if (!result.isSucceded)
                    {
                        TempData["ErrorMsg"] = result.errors[0].Description;
                        return RedirectToAction("Create");
                    }

                }
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
