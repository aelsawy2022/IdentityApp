using AutoMapper;
using IdentityApplication.Data.Entities;
using IdentityApplication.Data.Repositories.GovernorateRepo;
using IdentityApplication.Data.UnitOfWorks;
using IdentityApplication.Models;
using IdentityApplication.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static IdentityApplication.Models.Enums;

namespace IdentityApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGovernorateRepository _governorateRepository;
        public static IWebHostEnvironment _environment;

        public UsersController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IGovernorateRepository governorateRepository,
            IWebHostEnvironment environment
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _governorateRepository = governorateRepository;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> List(int currentPage = 1, int maxRows = 2)
        {
            UsersViewModel usersViewModel = new UsersViewModel();
            usersViewModel.Users = _mapper.Map<List<UsersModel>>(
                await _unitOfWork.UserRepository.GetAllAsync(o => o.OrderBy(u => u.UserName), "Governorate",
                                                             maxRows, (currentPage - 1) * maxRows) as List<User>);

            double pageCount = (double)((decimal)_userManager.Users.Count() / Convert.ToDecimal(maxRows));
            usersViewModel.PageCount = (int)Math.Ceiling(pageCount);
            usersViewModel.CurrentPage = currentPage;

            foreach (UsersModel user in usersViewModel.Users)
            {
                user.IsAdmin = _userManager.IsInRoleAsync(_mapper.Map<User>(user), SystemRoles.Admin.ToString()).Result;
                user.IsSuperAdmin = _userManager.IsInRoleAsync(_mapper.Map<User>(user), SystemRoles.SuperAdmin.ToString()).Result;
                if (user.Image != null) user.Image = Path.Combine(_environment.WebRootPath, "Upload", "Images", user.Image);
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

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(result);
        }

        [Authorize(Policy = "RequireSuperAdmin")]
        public IActionResult AddToSuperAdminRole(bool isSuperAdmin, string userName, int currentPage = 1, int maxRows = 2)
        {
            IdentityResult result;
            var _user = _userManager.FindByNameAsync(userName).Result;
            if (_user == null)
                return View();

            if (isSuperAdmin)
            {
                result = _userManager.RemoveFromRoleAsync(_user, SystemRoles.SuperAdmin.ToString()).Result;
            }
            else
            {
                var _role = _roleManager.FindByNameAsync(SystemRoles.SuperAdmin.ToString()).Result;
                if (_role == null)
                    return View();

                result = _userManager.AddToRoleAsync(_user, _role.Name).Result;
            }

            if (result.Succeeded)
                return RedirectToAction("List", new { currentPage = currentPage, maxRows = maxRows });

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(result);
        }

        public async Task<IActionResult> Edit(string userId)
        {
            UsersViewModel usersViewModel = new UsersViewModel();
            usersViewModel.User = _mapper.Map<UsersModel>(
                await _unitOfWork.UserRepository.GetOneAsync(
                    u => u.Id == userId, "Governorate"));

            usersViewModel.Governorates = await _governorateRepository.GetAllAsync(o => o.OrderBy(g => g.Name)) as List<Governorate>;
            return View(usersViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UsersModel user)
        {
            try
            {
                if (user.ImageFile == null)
                {
                    user.ImageFile = (IFormFile)Request.Form.Files;
                }
                if (!Directory.Exists(Path.Combine(_environment.WebRootPath, "Upload", "Images")))
                {
                    Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, "Upload", "Images"));
                }

                string fileName = DateTime.Now.ToString("MM-dd-yyyy_hmmsstt") + "_" + user.ImageFile.FileName;

                using (FileStream fileStream = System.IO.File.Create(Path.Combine(_environment.WebRootPath, "Upload", "Images", fileName)))
                {
                    await user.ImageFile.CopyToAsync(fileStream);
                    fileStream.Flush();
                }

                User u = await _unitOfWork.UserRepository.GetByIDAsync(user.Id);

                u.Image = user.ImageFile.FileName;
                u.Governorate = await _governorateRepository.GetByIDAsync(user.Governorate.Id);
                u.Name = user.Name;

                await _unitOfWork.UserRepository.UpdateAsync(u);
                await _unitOfWork.SaveAsync();
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
    }
}
