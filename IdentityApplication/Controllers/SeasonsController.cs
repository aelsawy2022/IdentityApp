using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using System;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class SeasonsController : Controller
    {
        private readonly ISeasonService _seasonService;

        public SeasonsController(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }

        public async Task<IActionResult> Index(Guid schoolId)
        {
            if (schoolId == null || schoolId == Guid.Empty) return RedirectToAction("Index", "Schools");

            return View(await _seasonService.Initiate(schoolId));
        }

        public async Task<IActionResult> Create(Guid schoolId)
        {
            return View(await _seasonService.InitiateCreate(schoolId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(SeasonModel season)
        {
            try
            {
                bool succeded = await _seasonService.Create(season);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index", new { schoolId = season.School.Id });
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
        public async Task<IActionResult> ActivateSeason(Guid seasonId, Guid schoolId)
        {
            try
            {
                bool succeded = await _seasonService.ActivateSeason(seasonId);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index", new { schoolId = schoolId });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }


        public async Task<IActionResult> Edit(Guid seasonId, Guid schoolId)
        {
            return View(await _seasonService.InitiateEdit(seasonId, schoolId));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SeasonModel season)
        {
            try
            {
                bool succeded = await _seasonService.Edit(season);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index", new { schoolId = season.School.Id });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

        public async Task<IActionResult> Delete(Guid seasonId, Guid schoolId)
        {
            try
            {
                bool succeded = await _seasonService.Delete(seasonId);
                if (!succeded) TempData["ErrorMsg"] = "Something wrong";
                return RedirectToAction("Index", new { schoolId = schoolId });
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
