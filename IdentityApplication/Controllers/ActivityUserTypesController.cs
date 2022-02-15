using IdentityApplication.Bases;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using System;

namespace IdentityApplication.Controllers
{
    public class ActivityUserTypesController : BaseController
    {
        private readonly IActivityService _activityService;
        private readonly IUserTypeService _userTypeService;

        public ActivityUserTypesController(IActivityService activityService, IUserTypeService userTypeService)
        {
            _activityService = activityService;
            _userTypeService = userTypeService;
        }

        //public async IActionResult Index(Guid activityId)
        //{
        //    try
        //    {
        //        if(activityId == Guid.Empty) return RedirectToAction("Index", "Activities");

        //        await
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
