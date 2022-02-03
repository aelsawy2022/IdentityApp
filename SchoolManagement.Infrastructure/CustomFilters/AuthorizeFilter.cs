using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SchoolManagement.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SchoolManagement.Infrastructure.CustomFilters
{
    public class AuthorizeFilter : IAuthorizationFilter
    {
        private readonly IUserService _userService;
        private readonly ISchoolService _schoolService;
        private readonly IActivityService _activityService;
        readonly string[] _claims;

        public AuthorizeFilter(
            IUserService userService, 
            ISchoolService schoolService, 
            IActivityService activityService, 
            params string[] claims)
        {
            _claims = claims;
            _userService = userService;
            _schoolService = schoolService;
            _activityService = activityService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            if (IsAuthenticated)
            {
                var user = _userService.GetByName(context.HttpContext.User.Identity.Name).Result;
                bool flagClaim = false;

                bool isSuperAdmin = user.Roles.Any(r => r.Name.ToLower() == Roles.SUPER_ADMIN.ToLower() && r.School == null && r.Activity == null);
                if (isSuperAdmin)
                {
                    flagClaim = true;
                }
                else
                {
                    if(_claims != null && _claims.Length > 0)
                    {
                        foreach (var claim in _claims)
                        {
                            switch (claim)
                            {
                                case Roles.ADMIN:
                                    flagClaim = user.Roles.Any(r => r.Name.ToLower() == Roles.ADMIN.ToLower() && r.School == null && r.Activity == null);
                                    break;

                                case Roles.SUPER_ADMIN:
                                    flagClaim = user.Roles.Any(r => r.Name.ToLower() == Roles.SUPER_ADMIN.ToLower() && r.School == null && r.Activity == null);
                                    break;

                                case Roles.SCHOOL_ADMIN:
                                    Guid schoolId = Guid.Parse(context.HttpContext.Request.Query["schoolId"]);
                                    flagClaim = user.Roles.Any(r => r.Name.ToLower() == Roles.ADMIN.ToLower() && (r.School != null ? r.School.Id == schoolId : false) && r.Activity == null);
                                    break;

                                case Roles.SCHOOL_SUPER_ADMIN:
                                    schoolId = Guid.Parse(context.HttpContext.Request.Query["schoolId"]);
                                    flagClaim = user.Roles.Any(r => r.Name.ToLower() == Roles.SUPER_ADMIN.ToLower() && (r.School != null ? r.School.Id == schoolId : false) && r.Activity == null);
                                    break;

                                case Roles.SCHOOL_ACTIVITY:
                                    Guid activityId = Guid.Parse(context.HttpContext.Request.Query["activityId"]);
                                    var activity = _activityService.GetById(activityId).Result;
                                    schoolId = Guid.Parse(context.HttpContext.Request.Query["schoolId"]);
                                    flagClaim = user.Roles.Any(r => r.Name.ToLower() == activity.Name.ToLower() && (r.School != null ? r.School.Id == schoolId : false) && (r.Activity != null ? r.Activity.Id == activityId : false));
                                    break;

                                default:
                                    flagClaim = true;
                                    break;
                            }
                            //if (context.HttpContext.User.HasClaim("ACCESS_LEVEL", claim))
                            //{
                            //    flagClaim = true;
                            //    break;
                            //}
                        }
                    }
                    else
                    {
                        flagClaim = true;
                    }
                }
                if (!flagClaim)
                {
                    var result = new ViewResult { ViewName = "Error" };
                    var modelMetadata = new EmptyModelMetadataProvider();
                    result.ViewData = new ViewDataDictionary(modelMetadata, context.ModelState);
                    result.ViewData.Add("ErrorMsg", "Access Denied, You can't procced in this action");
                    context.Result = result;
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }

            }
            else
            {
                context.Result = new ViewResult() { ViewName = "/Identity/Account/Login" };
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}