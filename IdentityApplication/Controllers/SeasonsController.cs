using IdentityApplication.Data.Entities;
using IdentityApplication.Data.UnitOfWorks;
using IdentityApplication.Models;
using IdentityApplication.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class SeasonsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SeasonsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(Guid schoolId)
        {
            if (schoolId == null || schoolId == Guid.Empty) return RedirectToAction("Index", "Schools");

            SeasonViewModel seasonViewModel = new SeasonViewModel();
            seasonViewModel.Seasons = await _unitOfWork.SeasonRepository.GetAsync(a => a.School.Id == schoolId, o => o.OrderBy(a => a.CreationDate)) as List<Season>;
            seasonViewModel.School = await _unitOfWork.SchoolRepository.GetByIDAsync(schoolId);
            return View(seasonViewModel);
        }

        public async Task<IActionResult> Create(Guid schoolId)
        {
            SeasonViewModel activityViewModel = new SeasonViewModel();
            activityViewModel.Season = new Season();
            activityViewModel.School = await _unitOfWork.SchoolRepository.GetByIDAsync(schoolId);
            return View(activityViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Season season)
        {
            try
            {
                season.CreationDate = DateTime.Now;
                season.Id = Guid.NewGuid();
                season.School = await _unitOfWork.SchoolRepository.GetByIDAsync(season.School.Id);

                await _unitOfWork.SeasonRepository.AddAsync(season);
                await _unitOfWork.SaveAsync();
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

                Season season = await _unitOfWork.SeasonRepository.GetByIDAsync(seasonId);
                if (season == null)
                {
                    ViewBag.Error = "Type not found";
                    return View("Index");
                }
                season.Current = true;
                await _unitOfWork.SeasonRepository.UpdateAsync(season);

                Season currentSeason = await _unitOfWork.SeasonRepository.GetOneAsync(s => s.Current);
                if (currentSeason != null)
                {
                    currentSeason.Current = false;
                    await _unitOfWork.SeasonRepository.UpdateAsync(currentSeason);
                }

                await _unitOfWork.SaveAsync();
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
            SeasonViewModel seasonViewModel = new SeasonViewModel();
            seasonViewModel.Season = await _unitOfWork.SeasonRepository.GetByIDAsync(seasonId);
            seasonViewModel.School = await _unitOfWork.SchoolRepository.GetByIDAsync(schoolId);
            return View(seasonViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Season season)
        {
            try
            {
                season.School = await _unitOfWork.SchoolRepository.GetByIDAsync(season.School.Id);

                await _unitOfWork.SeasonRepository.UpdateAsync(season);
                await _unitOfWork.SaveAsync();
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
                Season season = await _unitOfWork.SeasonRepository.GetByIDAsync(seasonId);
                if (season == null)
                {
                    ViewBag.Error = "Not Found";
                    return View("Index");
                }

                await _unitOfWork.SeasonRepository.DeleteAsync(season);
                await _unitOfWork.SaveAsync();
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
