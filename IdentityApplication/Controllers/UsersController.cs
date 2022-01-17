using AutoMapper;
using IdentityApplication.Data.Entities;
using IdentityApplication.Models;
using IdentityApplication.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static IdentityApplication.Models.Enums;

namespace IdentityApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        public UsersController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult List(int currentPage = 1, int maxRows = 2)
        {
            UsersViewModel usersViewModel = new UsersViewModel();
            usersViewModel.Users = _mapper.Map<List<UsersModel>>(
                _userManager.Users.OrderBy(o => o.Id).Skip((currentPage - 1) * maxRows).Take(maxRows).ToList());

            double pageCount = (double)((decimal)_userManager.Users.Count() / Convert.ToDecimal(maxRows));
            usersViewModel.PageCount = (int)Math.Ceiling(pageCount);
            usersViewModel.CurrentPage = currentPage;

            foreach (UsersModel user in usersViewModel.Users)
            {
                user.IsAdmin = _userManager.IsInRoleAsync(_mapper.Map<User>(user), SystemRoles.Admin.ToString()).Result;
            }
            usersViewModel.Roles = _roleManager.Roles.ToList();

            return View(usersViewModel);
        }

        [Authorize(Policy = "RequireSuperAdmin")]
        public IActionResult MakeAsAdmin(bool isAdmin, string userName, int currentPage = 1, int maxRows = 2)
        {
            IdentityResult result;
            var _user = _userManager.FindByNameAsync(userName).Result;
            if (_user == null)
                return View();

            if (isAdmin)
            {
                result = _userManager.RemoveFromRoleAsync(_user, SystemRoles.Admin.ToString()).Result;
            }
            else
            {
                var _role = _roleManager.FindByNameAsync(SystemRoles.Admin.ToString()).Result;
                if (_role == null)
                    return View();

                result = _userManager.AddToRoleAsync(_user, _role.Name).Result;
            }

            if (result.Succeeded)
                return RedirectToAction("List", new { currentPage = currentPage, maxRows = maxRows });

            foreach(IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(result);
        }
    }
}
