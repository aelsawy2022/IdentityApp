using IdentityApplication.Data.Entities;
using IdentityApplication.Data.Repositories.GovernorateRepo;
using IdentityApplication.Data.Repositories.ManagementRepo;
using IdentityApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class ManagementsController : Controller
    {
        private readonly IManagementRepository _managementRepository;
        private readonly IGovernorateRepository _governorateRepository;

        public ManagementsController(
            IManagementRepository managementRepository,
            IGovernorateRepository governorateRepository
            )
        {
            _managementRepository = managementRepository;
            _governorateRepository = governorateRepository;
        }

        public async Task<IActionResult> Index()
        {
            ManagementViewModel managementViewModel = new ManagementViewModel();
            managementViewModel.Managements = await _managementRepository.GetAllAsync() as List<Management>;
            return View(managementViewModel);
        }

        public async Task<IActionResult> Create()
        {
            ManagementViewModel managementViewModel = new ManagementViewModel();
            managementViewModel.Management = new Management();
            managementViewModel.Governorates = await _governorateRepository.GetAllAsync() as List<Governorate>;
            return View(managementViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Management management)
        {
            try
            {
                management.CreationDate = DateTime.Now;
                management.Id = Guid.NewGuid();
                Governorate governorate = await _governorateRepository.GetByIDAsync(management.Governorate.Id);
                management.Governorate = governorate;
                await _managementRepository.AddAsync(management);
                await _managementRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ViewBag.Error = ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }
    }
}
