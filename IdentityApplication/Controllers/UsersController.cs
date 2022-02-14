using IdentityApplication.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.ViewModels.ViewModels;
using System;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class UsersController : BaseController
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
            try
            {
                return View(await _userService.GetUsers(currentPage, maxRows));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize(Policy = "RequireSuperAdmin")]
        public async Task<IActionResult> MakeAsAdmin(bool isAdmin, string userName, int currentPage = 1, int maxRows = 2)
        {
            try
            {
                bool succeded = await _userService.AddToAdminRole(userName, isAdmin);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";

                return RedirectToAction("List", new { currentPage = currentPage, maxRows = maxRows });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize(Policy = "RequireSuperAdmin")]
        public async Task<IActionResult> AddToSuperAdminRole(bool isSuperAdmin, string userName, int currentPage = 1, int maxRows = 2)
        {
            try
            {
                bool succeded = await _userService.AddToSuperAdminRole(userName, isSuperAdmin);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("List", new { currentPage = currentPage, maxRows = maxRows });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> Edit(Guid userId)
        {
            try
            {
                return View(await _userService.InitiateEdit(userId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserVM usersViewModel)
        {
            try
            {
                bool succeded = await _userService.Edit(usersViewModel);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("List");
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
                return View(await _userService.InitiateCreate());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserVM usersViewModel)
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
