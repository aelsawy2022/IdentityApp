using AutoMapper;
using IdentityApplication.Data.Entities;
using IdentityApplication.Data.UnitOfWorks;
using IdentityApplication.Models;
using IdentityApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class ClassUsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassUsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(Guid gradeId, Guid schoolId, Guid classId)
        {
            if (classId == Guid.Empty) return RedirectToAction("Index", "Classes", new { gradeId = gradeId, schoolId = schoolId });

            ClassUserViewModel classUserViewModel = new ClassUserViewModel();
            classUserViewModel.usersFilter = new UsersFilter();

            var classUsers = (await _unitOfWork.ClassUserRepository.GetAsync(cu => cu.ClassId == classId,
                                                o => o.OrderBy(cu => cu.UserId), "User,UserType,Season") as List<ClassUser>).ToList();
            classUserViewModel.ClassUsers = _mapper.Map<List<ClassUsersModel>>(classUsers);

            var allUsers = await _unitOfWork.UserRepository.GetAsync(GenerateAllUsersExpression(classId), o => o.OrderBy(u => u.Name), "ClassUsers") as List<User>;
            classUserViewModel.AllUsers = _mapper.Map<List<UsersModel>>(allUsers);
            //var allUsers = await _unitOfWork.UserRepository.GetAllAsync(o => o.OrderBy(u => u.UserName)) as List<User>;
            //classUserViewModel.AllUsers = _mapper.Map<List<UsersModel>>(allUsers);

            classUserViewModel.Types = await _unitOfWork.UserTypeRepository.GetAllAsync(o => o.OrderBy(ut => ut.Name)) as List<UserType>;

            classUserViewModel.Class = await _unitOfWork.ClassRepository.GetByIDAsync(classId);
            classUserViewModel.SchoolId = schoolId;
            classUserViewModel.GradeId = gradeId;

            return View(classUserViewModel);
        }

        private Expression<Func<User, bool>> GenerateAllUsersExpression(Guid classId)
        {
            Expression<Func<User, bool>> _Expression = (
                x => (x.ClassUsers == null || x.ClassUsers.Count == 0)
                || (!x.ClassUsers.Any(cu => cu.ClassId == classId))
            );
            return _Expression;
        }

        public async Task<IActionResult> AddUsers(ClassUserViewModel model)
        {
            try
            {
                Season schoolCurrentSeason = await _unitOfWork.SeasonRepository.GetOneAsync(s => s.Current && s.School.Id == model.SchoolId);

                if(schoolCurrentSeason == null)
                {
                    TempData["ErrorMsg"] = "No Seasons added to this school yet, Please add season first and try again";
                }
                else if (model.SelectedTypeId == null || model.SelectedTypeId == Guid.Empty)
                {
                    TempData["ErrorMsg"] = "You must select user type";
                }
                else
                {
                    foreach (UsersModel user in model.AllUsers.Where(u => u.IsSelected))
                    {
                        ClassUser _classUser = new ClassUser();
                        _classUser.UserId = user.Id;
                        _classUser.UserTypeId = model.SelectedTypeId;
                        _classUser.ClassId = model.Class.Id;
                        _classUser.SeasonId = schoolCurrentSeason.Id;

                        await _unitOfWork.ClassUserRepository.AddAsync(_classUser);
                    }
                    await _unitOfWork.SaveAsync();
                }
                return RedirectToAction("Index", new { gradeId = model.GradeId, schoolId = model.SchoolId, classId = model.Class.Id });
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

        public async Task<IActionResult> DeleteUser(ClassUserViewModel model)
        {
            try
            {
                foreach (ClassUsersModel classUser in model.ClassUsers.Where(u => u.User.IsSelected))
                {
                    await _unitOfWork.ClassUserRepository.DeleteAsync(await _unitOfWork.ClassUserRepository.GetOneAsync(cu => cu.UserId == classUser.User.Id
                                                                        && cu.ClassId == model.Class.Id
                                                                        && cu.SeasonId == classUser.SeasonId
                                                                        && cu.UserTypeId == classUser.UserTypeId));
                }
                await _unitOfWork.ClassUserRepository.SaveAsync();
                return RedirectToAction("Index", new { gradeId = model.GradeId, schoolId = model.SchoolId, classId = model.Class.Id });
            }
            catch(Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.ErrorMessage = ex.Message.ToString();
                return View("Error", errorViewModel);
            }
        }

        public async Task<IActionResult> AddAllUsers(ClassUserViewModel model)
        {
            foreach(UsersModel user in model.AllUsers)
            {
                user.IsSelected = true;
            }
            return await AddUsers(model);
        }

        public async Task<IActionResult> DeleteAllUsers(ClassUserViewModel model)
        {
            foreach (ClassUsersModel user in model.ClassUsers)
            {
                user.User.IsSelected = true;
            }
            return await DeleteUser(model);
        }
    }
}
