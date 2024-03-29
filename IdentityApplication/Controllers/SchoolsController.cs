﻿using IdentityApplication.Bases;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Infrastructure.CustomFilters;
using SchoolManagement.Models.Models;
using System;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class SchoolsController : BaseController
    {
        private readonly ISchoolService _schoolService;

        public SchoolsController(
            ISchoolService schoolService
            )
        {
            _schoolService = schoolService;
        }

        [Authorize(Roles.ADMIN)]
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _schoolService.GetAllSchools());
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
                return View(await _schoolService.InitiateCreate());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(SchoolModel school)
        {
            try
            {
                bool succeded = await _schoolService.Create(school);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize(Roles.ADMIN, Roles.SCHOOL_SUPER_ADMIN)]
        public async Task<IActionResult> Edit(Guid schoolId)
        {
            try
            {
                return View(await _schoolService.InitiateEdit(schoolId));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SchoolModel school)
        {
            try
            {
                bool succeded = await _schoolService.Edit(school);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Authorize(Roles.SUPER_ADMIN, Roles.SCHOOL_SUPER_ADMIN)]
        public async Task<IActionResult> Delete(Guid schoolId)
        {
            try
            {
                bool succeded = await _schoolService.Delete(schoolId);
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
