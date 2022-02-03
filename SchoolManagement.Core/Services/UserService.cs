using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.ViewModels.ViewModels;
using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.Repositories.GovernorateRepo;
using SchoolManagement.Persistance.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static SchoolManagement.Models.Models.Enums;

namespace SchoolManagement.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGovernorateRepository _governorateRepository;
        public static IWebHostEnvironment _environment;

        public UserService(
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

        public Task<bool> Create(UserVM model)
        {
            throw new NotImplementedException();
        }

        public async Task<UserServiceResponse> CreateUser(UserVM usersViewModel)
        {
            string userPass = "Test@123";
            string userEmail = usersViewModel.User.Name.Trim().Replace(" ", ".").ToLower() + usersViewModel.ServerName;

            var user = new User
            {
                UserName = userEmail,
                Email = userEmail,
                Name = usersViewModel.User.Name,
                Governorate = await _governorateRepository.GetByIDAsync(usersViewModel.User.Governorate.Id),
                Active = true,
                EmailConfirmed = true
            };

            AssignRolesToUser(user, usersViewModel);

            var result = await _userManager.CreateAsync(user, userPass);

            return new UserServiceResponse { isSucceded = result.Succeeded, errors = result.Errors.ToList() };
        }

        public Task<bool> Delete(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Edit(UserVM usersViewModel)
        {

            User u = await _unitOfWork.UserRepository.GetOneAsync(u => u.Id == usersViewModel.User.Id, "UserRoles");

            AssignRolesToUser(u, usersViewModel);

            if (usersViewModel.User.ImageFile != null)
            {
                DeleteCurrentImageIfExists(u.Image);
                u.Image = await UploadImage(usersViewModel.User.ImageFile);
            }
            u.Governorate = await _governorateRepository.GetByIDAsync(usersViewModel.User.Governorate.Id);
            u.Name = usersViewModel.User.Name;

            await _unitOfWork.UserRepository.UpdateAsync(u);
            await _unitOfWork.SaveAsync();

            return true;
        }

        private void AssignRolesToUser(User user, UserVM usersViewModel)
        {
            user.UserRoles = new List<UserRole>();

            if (usersViewModel.Roles != null)
            {
                foreach (RolesModel gr in usersViewModel.Roles)
                {
                    if (gr.IsSelected)
                    {
                        user.UserRoles.Add(new UserRole() { RoleId = gr.Id, UserId = user.Id });
                    }
                }
            }

            if (usersViewModel.Schools != null)
            {
                foreach (SchoolModel s in usersViewModel.Schools)
                {
                    if (s.Roles != null)
                    {
                        foreach (RolesModel sr in s.Roles)
                        {
                            if (sr.IsSelected)
                            {
                                user.UserRoles.Add(new UserRole() { RoleId = sr.Id, UserId = user.Id });
                            }
                        }
                    }
                }
            }
        }

        private void DeleteCurrentImageIfExists(string image)
        {
            if (!string.IsNullOrEmpty(image))
            {
                string imagePath = Path.Combine(_environment.WebRootPath, "Upload", "Images", image);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
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

        public Task<UserVM> Initiate(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public async Task<UserVM> GetUsers(int currentPage, int maxRows)
        {
            UserVM usersViewModel = new UserVM();
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

            return usersViewModel;
        }

        public async Task<bool> AddToSuperAdminRole(string userName, bool isSuperAdmin)
        {
            IdentityResult result;

            var _user = await _userManager.FindByNameAsync(userName);
            if (_user == null) return false;

            if (isSuperAdmin)
            {
                result = await _userManager.RemoveFromRoleAsync(_user, SystemRoles.SuperAdmin.ToString());
            }
            else
            {
                var _role = await _roleManager.FindByNameAsync(SystemRoles.SuperAdmin.ToString());
                if (_role == null) return false;

                result = await _userManager.AddToRoleAsync(_user, _role.Name);
            }

            return result.Succeeded;
        }

        public async Task<bool> AddToAdminRole(string userName, bool isAdmin)
        {
            IdentityResult result;
            var _user = await _userManager.FindByNameAsync(userName);
            if (_user == null) return false;

            if (isAdmin)
            {
                result = await _userManager.RemoveFromRoleAsync(_user, SystemRoles.Admin.ToString());
            }
            else
            {
                var _role = await _roleManager.FindByNameAsync(SystemRoles.Admin.ToString());
                if (_role == null) return false;

                result = await _userManager.AddToRoleAsync(_user, _role.Name);
            }

            return result.Succeeded;
        }

        public async Task<UserVM> InitiateCreate(params object[] arguments)
        {
            UserVM usersViewModel = new UserVM();

            usersViewModel.User = new UsersModel();

            usersViewModel.Schools = _mapper.Map<List<SchoolModel>>(await _unitOfWork.SchoolRepository.GetSchoolWithActivitiesAndRolesAsync(null, o => o.OrderBy(s => s.Name)) as List<School>);
            usersViewModel.Roles = _mapper.Map<List<RolesModel>>(await _unitOfWork.RoleRepository.GetAsync(r => r.School == null && r.Activity == null, o => o.OrderBy(r => r.Name)) as List<Role>);

            usersViewModel.Governorates = _mapper.Map<List<GovernorateModel>>(await _governorateRepository.GetAllAsync(o => o.OrderBy(g => g.Name)) as List<Governorate>);

            return usersViewModel;
        }

        public async Task<UserVM> InitiateEdit(params object[] arguments)
        {
            try
            {
                UserVM usersViewModel = new UserVM();
                usersViewModel.User = _mapper.Map<UsersModel>((await _unitOfWork.UserRepository.GetUsersWithRolesAsync(
                                                                        u => u.Id == (Guid)arguments[0], null) as List<User>).FirstOrDefault());


                usersViewModel.Schools = _mapper.Map<List<SchoolModel>>(await _unitOfWork.SchoolRepository.GetSchoolWithActivitiesAndRolesAsync(null, o => o.OrderBy(s => s.Name)) as List<School>);
                usersViewModel.Roles = _mapper.Map<List<RolesModel>>(await _unitOfWork.RoleRepository.GetAsync(r => r.School == null && r.Activity == null, o => o.OrderBy(r => r.Name)) as List<Role>);

                usersViewModel.Governorates = _mapper.Map<List<GovernorateModel>>(await _governorateRepository.GetAllAsync(o => o.OrderBy(g => g.Name)) as List<Governorate>);

                SyncUserRolesWithSystemRoles(usersViewModel);

                return usersViewModel;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private void SyncUserRolesWithSystemRoles(UserVM usersViewModel)
        {
            foreach (SchoolModel school in usersViewModel.Schools)
            {
                foreach (RolesModel schoolRole in school.Roles)
                {
                    //Fe moshkla hena bos 3liha
                    if (usersViewModel.User.Roles.Any() && usersViewModel.User.Roles.Any(ur => ur.Id == schoolRole.Id))
                        schoolRole.IsSelected = true;
                }
                foreach (Models.Models.ActivityModel activity in school.Activities)
                {
                    foreach (RolesModel activityRole in activity.Roles)
                    {
                        if (usersViewModel.User.Roles.Any(ur => ur.Id == activityRole.Id))
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
            foreach (RolesModel generalRole in usersViewModel.Roles)
            {
                if (usersViewModel.User.Roles.Any(ur => ur.Id == generalRole.Id))
                    generalRole.IsSelected = true;
            }
            if (!usersViewModel.Roles.Any(gr => !gr.IsSelected))
                usersViewModel.IsGeneralRolesSelected = true;

            if (!usersViewModel.Schools.Any(s => !s.IsSelected))
                usersViewModel.IsSchoolsRolesSelected = true;
        }

        public async Task<UsersModel> GetByName(string username)
        {
            User user = await _unitOfWork.UserRepository.GetOneAsync(u => u.UserName == username || u.Name == username, "Governorate,UserRoles");
            foreach(UserRole ur in user.UserRoles)
            {
                ur.Role = await _unitOfWork.RoleRepository.GetOneAsync(r => r.Id == ur.RoleId, "School,Activity");
            }
            return _mapper.Map<UsersModel>(user);
        }

        public async Task<UsersModel> GetById(Guid id)
        {
            return _mapper.Map<UsersModel>((await _unitOfWork.UserRepository.GetUsersWithRolesAsync(u => u.Id == id) as List<User>).FirstOrDefault());
        }
    }

    public class UserServiceResponse
    {
        public bool isSucceded { get; set; }
        public List<IdentityError> errors { get; set; }
    }
}
