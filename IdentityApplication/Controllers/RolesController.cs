using IdentityApplication.Bases;
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
    public class RolesController : BaseController
    {
        private readonly RoleManager<Role> _roleManager;

        public RolesController(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            try
            {
                var roles = _roleManager.Roles.Where(r => r.School == null && r.Activity == null).ToList();
                return View(roles);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult Create()
        {
            return View(new Role());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Role role)
        {
            try
            {
                await _roleManager.CreateAsync(role);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> Edit(string roleId)
        {
            try
            {
                return View(await _roleManager.FindByIdAsync(roleId));
            }
            catch (Exception ex)
            {
                throw;
            }
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
                throw;
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
                throw;
            }
        }
    }
}
