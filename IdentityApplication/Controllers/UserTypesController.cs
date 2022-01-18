using IdentityApplication.Data.Entities;
using IdentityApplication.Data.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    public class UserTypesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.UserTypeRepository.GetAllAsync(o => o.OrderBy(ut => ut.CreationDate)) as List<UserType>);
        }

        public IActionResult Create()
        {
            return View(new UserType());
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserType userType)
        {
            try
            {
                userType.CreationDate = DateTime.Now;
                userType.Id = Guid.NewGuid();
                await _unitOfWork.UserTypeRepository.AddAsync(userType);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ViewBag.Error = ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }


        [Authorize(Policy = "RequireSuperAdmin")]
        public async Task<IActionResult> ActivateType(Guid typeId)
        {
            try
            {

                UserType type = await _unitOfWork.UserTypeRepository.GetByIDAsync(typeId);
                if (type == null)
                {
                    ViewBag.Error = "Type not found";
                    return View("Index");
                }

                type.Active = !type.Active;
                await _unitOfWork.UserTypeRepository.UpdateAsync(type);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ViewBag.Error = ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid userTypeId)
        {
            return View(await _unitOfWork.UserTypeRepository.GetByIDAsync(userTypeId));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserType userType)
        {
            try
            {
                await _unitOfWork.UserTypeRepository.UpdateAsync(userType);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                ViewBag.Error = ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid userTypeId)
        {
            try
            {
                UserType userType = await _unitOfWork.UserTypeRepository.GetByIDAsync(userTypeId);
                if (userType == null)
                {
                    ViewBag.Error = "Not Found";
                    return View("Index");
                }

                await _unitOfWork.UserTypeRepository.DeleteAsync(userType);
                await _unitOfWork.SaveAsync();
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
