using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.ViewModels.ViewModels;
using System;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class ClassUsersController : Controller
    {
        private readonly IClassUserService _classUserService;

        public ClassUsersController(IClassUserService classUserService)
        {
            _classUserService = classUserService;
        }

        public async Task<IActionResult> Index(Guid gradeId, Guid schoolId, Guid classId)
        {
            if (classId == Guid.Empty) return RedirectToAction("Index", "Classes", new { gradeId = gradeId, schoolId = schoolId });

            return View(await _classUserService.Initiate(gradeId, schoolId, classId));
        }

        public async Task<IActionResult> AddUsers(ClassUserVM model)
        {
            try
            {
                var result = await _classUserService.AddUsers(model);
                if(!result.isSucceded) TempData["ErrorMsg"] = result.message.ToString();
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

        public async Task<IActionResult> DeleteUser(ClassUserVM model)
        {
            try
            {
                var result = await _classUserService.DeleteUsers(model);
                if (!result.isSucceded) TempData["ErrorMsg"] = result.message.ToString();
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

        public async Task<IActionResult> AddAllUsers(ClassUserVM model)
        {
            foreach(UsersModel user in model.AllUsers)
            {
                user.IsSelected = true;
            }
            return await AddUsers(model);
        }

        public async Task<IActionResult> DeleteAllUsers(ClassUserVM model)
        {
            foreach (ClassUsersModel user in model.ClassUsers)
            {
                user.User.IsSelected = true;
            }
            return await DeleteUser(model);
        }
    }
}
