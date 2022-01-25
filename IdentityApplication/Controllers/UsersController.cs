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
using System.Linq.Expressions;
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

        public IActionResult Index(UsersFilter filter)
        {
            //Expression<Func<User, bool>> _Expression = null;

            //if (filter != null)
            //{
            //    _Expression =
            //    (
            //        x => (!string.IsNullOrEmpty(filter.Name) ? (x.Name == filter.Name || x.UserName == filter.Name) : true)
            //        && (!string.IsNullOrEmpty(filter.Id) ? x.Id == filter.Id : true)
            //    );
            //}

            //await _unitOfWork.
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
            }
            //usersViewModel.Roles = _roleManager.Roles.ToList();

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

        public async Task<IActionResult> Edit(Guid userId)
        {
            UsersViewModel usersViewModel = new UsersViewModel();
            usersViewModel.User = _mapper.Map<UsersModel>(await _unitOfWork.UserRepository.GetOneAsync(
                                                                    u => u.Id == userId, "Governorate,UserRoles"));


            usersViewModel.Schools = _mapper.Map<List<SchoolModel>>(await _unitOfWork.SchoolRepository.GetSchoolWithActivitiesAndRolesAsync(null, o => o.OrderBy(s => s.Name)) as List<School>);
            usersViewModel.Roles = _mapper.Map<List<RolesModel>>(await _unitOfWork.RoleRepository.GetAsync(r => r.School == null && r.Activity == null, o => o.OrderBy(r => r.Name)) as List<Role>);
            //usersViewModel.Activities = _mapper.Map<List<ActivityModel>>(await _unitOfWork.ActivityRepository.GetAllAsync(o => o.OrderBy(a => a.Name), "Roles") as List<Activity>);

            usersViewModel.Governorates = await _governorateRepository.GetAllAsync(o => o.OrderBy(g => g.Name)) as List<Governorate>;

            SyncUserRolesWithSystemRoles(usersViewModel);

            return View(usersViewModel);
        }

        private void SyncUserRolesWithSystemRoles(UsersViewModel usersViewModel)
        {
            foreach(SchoolModel school in usersViewModel.Schools)
            {
                foreach(RolesModel schoolRole in school.Roles)
                {
                    if (usersViewModel.User.Roles.Any() && usersViewModel.User.Roles.Any(ur => ur.RoleId == schoolRole.Id))
                        schoolRole.IsSelected = true;
                }
                foreach(ActivityModel activity in school.Activities)
                {
                    foreach(RolesModel activityRole in activity.Roles)
                    {
                        if (usersViewModel.User.Roles.Any(ur => ur.RoleId == activityRole.Id))
                            activityRole.IsSelected = true;
                    }
                    if (activity.Roles.Any() && !activity.Roles.Any(ar => !ar.IsSelected))
                        activity.IsSelected = true;
                }
                if (school.Roles.Any() && !school.Roles.Any(sr => !sr.IsSelected))
                    school.IsSelected = true;

                if (school.Activities.Any() && !school.Activities.Any(a => !a.IsSelected))
                    school.IsActivitiesSelected = true;
            }
            foreach(RolesModel generalRole in usersViewModel.Roles)
            {
                if (usersViewModel.User.Roles.Any(ur => ur.RoleId == generalRole.Id))
                    generalRole.IsSelected = true;
            }
            if (!usersViewModel.Roles.Any(gr => !gr.IsSelected))
                usersViewModel.IsGeneralRolesSelected = true;

            if (!usersViewModel.Schools.Any(s => !s.IsSelected))
                usersViewModel.IsSchoolsRolesSelected = true;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UsersModel user)
        {
            try
            {
                User u = await _unitOfWork.UserRepository.GetByIDAsync(user.Id);

                if(user.ImageFile != null)
                {
                    DeleteCurrentImageIfExists(u.Image);
                }
                u.Image = await UploadImage(user.ImageFile);
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

        private async Task<string> UploadImage(IFormFile image)
        {
            if (!Directory.Exists(Path.Combine(_environment.WebRootPath, "Upload", "Images")))
            {
                Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, "Upload", "Images"));
            }

            string fileName = DateTime.Now.ToString("MM-dd-yyyy_hmmsstt") + "_" + image.FileName;

            using (FileStream fileStream = System.IO.File.Create(Path.Combine(_environment.WebRootPath, "Upload", "Images", fileName)))
            {
                await image.CopyToAsync(fileStream);
                fileStream.Flush();
            }
            return fileName;
        }

        private void DeleteCurrentImageIfExists(string image)
        {
            string imagePath = Path.Combine(_environment.WebRootPath, "Upload", "Images", image);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        public async Task<IActionResult> ClassUsers(Guid schoolId)
        {
            //if (schoolId == null || schoolId == Guid.Empty) return View("List");

            //School school = await _unitOfWork.SchoolRepository.GetByIDAsync(schoolId);
            //school.use
            //_unitOfWork.UserRepository.GetAsync(u => u.ClassUsers.)
            return View();
        }
    }
}
