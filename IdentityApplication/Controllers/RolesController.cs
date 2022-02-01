using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Models;
using SchoolManagement.Persistance.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    [Authorize(Policy = "RequireSuperAdmin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<Role> _roleManager;

        public RolesController(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.Where(r => r.School == null && r.Activity == null).ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View(new Role());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Role role)
        {
            await _roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string roleId)
        {
            return View(await _roleManager.FindByIdAsync(roleId));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Role role)
        {
            try
            {
                await _roleManager.UpdateAsync(role);
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

        public async Task<IActionResult> Delete(string roleId)
        {
            try
            {
                Role role = await _roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    ViewBag.Error = "Not Found";
                    return View("Index");
                }

                await _roleManager.DeleteAsync(role);
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
