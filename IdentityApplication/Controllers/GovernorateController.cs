using IdentityApplication.Data.Entities;
using IdentityApplication.Data.Repositories.GovernorateRepo;
using IdentityApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class GovernorateController : Controller
    {
        private readonly IGovernorateRepository _governorateRepository;
        public GovernorateController(
            IGovernorateRepository governorateRepository
            )
        {
            _governorateRepository = governorateRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _governorateRepository.GetAllAsync() as List<Governorate>);
        }

        public IActionResult Create()
        {
            return View(new Governorate());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Governorate governorate)
        {
            try
            {
                governorate.CreationDate = DateTime.Now;
                governorate.Id = Guid.NewGuid();
                await _governorateRepository.AddAsync(governorate);
                await _governorateRepository.SaveAsync();
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
            return View(await _governorateRepository.GetByIDAsync(governorateId));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Governorate governorate)
        {
            try
            {
                await _governorateRepository.UpdateAsync(governorate);
                await _governorateRepository.SaveAsync();
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
                Governorate governorate = await _governorateRepository.GetByIDAsync(governorateId);
                if (governorate == null)
                {
                    ViewBag.Error = "Not Found";
                    return View("Index");
                }

                await _governorateRepository.DeleteAsync(governorate);
                await _governorateRepository.SaveAsync();
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
