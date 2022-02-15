using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using SchoolManagement.Core.Services;
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
        private readonly IGradesService _gradesService;
        readonly string[] _claims;

        public AuthorizeFilter(
            IUserService userService,
            ISchoolService schoolService,
            IActivityService activityService,
            IGradesService gradesService,
            params string[] claims)
        {
            _claims = claims;
            _userService = userService;
            _schoolService = schoolService;
            _activityService = activityService;
            _gradesService = gradesService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                bool isApiRequest = context.HttpContext.Request.Path.Value.StartsWith("/api");
                var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
                if (IsAuthenticated)
                {
                    bool flagClaim = false;

                    var user = _userService.GetByName(context.HttpContext.User.Identity.Name).Result;

                    bool isSuperAdmin = user.Roles.Any(r => r.Name.ToLower() == Roles.SUPER_ADMIN.ToLower() && r.School == null && r.Activity == null);

                    if (isSuperAdmin)
                    {
                        flagClaim = true;
                    }
                    else
                    {

                        if (_claims != null && _claims.Length > 0)
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
                                        Guid schoolId = isApiRequest ? GetSchoolIdForApiRequest(context.RouteData) : GetSchoolId(context.HttpContext.Request);
                                        flagClaim = user.Roles.Any(r => r.Name.ToLower() == Roles.ADMIN.ToLower() && (r.School != null ? r.School.Id == schoolId : false) && r.Activity == null);
                                        break;

                                    case Roles.SCHOOL_SUPER_ADMIN:
                                        schoolId = isApiRequest ? GetSchoolIdForApiRequest(context.RouteData) : GetSchoolId(context.HttpContext.Request);
                                        flagClaim = user.Roles.Any(r => r.Name.ToLower() == Roles.SUPER_ADMIN.ToLower() && (r.School != null ? r.School.Id == schoolId : false) && r.Activity == null);
                                        break;

                                    case Roles.SCHOOL_ACTIVITY:
                                        Guid activityId = isApiRequest ? Guid.Parse(context.RouteData.Values["activityId"].ToString()) : Guid.Parse(context.HttpContext.Request.Query["activityId"]);
                                        var activity = _activityService.GetById(activityId).Result;
                                        schoolId = isApiRequest ? GetSchoolIdForApiRequest(context.RouteData) : GetSchoolId(context.HttpContext.Request);
                                        flagClaim = user.Roles.Any(r => r.Name.ToLower() == activity.Name?.ToLower() && (r.School != null ? r.School.Id == schoolId : false) && (r.Activity != null ? r.Activity.Id == activityId : false));
                                        break;

                                    default:
                                        flagClaim = true;
                                        break;
                                }
                                if (!flagClaim) break;
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
                        string responseMessage = "Access Denied, You can't procced in this action";
                        if (isApiRequest)
                        {
                            context.Result = new JsonResult(new { Error = responseMessage });
                            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            var result = new ViewResult { ViewName = "Error" };
                            var modelMetadata = new EmptyModelMetadataProvider();
                            result.ViewData = new ViewDataDictionary(modelMetadata, context.ModelState);
                            result.ViewData.Add("ErrorMsg", responseMessage);
                            context.Result = result;
                            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                    }

                }
                else
                {
                    if (isApiRequest)
                    {
                        context.Result = new ForbidResult();
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    }
                    else
                    {
                        context.Result = new ViewResult() { ViewName = "/Identity/Account/Login" };
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private Guid GetSchoolId(HttpRequest request)
        {
            if (!string.IsNullOrEmpty(request.Query["schoolId"]))
            {
                return Guid.Parse(request.Query["schoolId"]);
            }
            else if (!string.IsNullOrEmpty(request.Query["gradeId"]))
            {
                return (_gradesService.GetGradeSchool(request.Query["gradeId"]).Result).Id;
            }
            return Guid.Empty;
        }

        private Guid GetSchoolIdForApiRequest(RouteData routeData)
        {
            if (routeData.Values["schoolId"] != null)
            {
                return Guid.Parse(routeData.Values["schoolId"].ToString());
            }
            else if (routeData.Values["gradeId"] != null)
            {
                return (_gradesService.GetGradeSchool(routeData.Values["gradeId"].ToString()).Result).Id;
            }
            return Guid.Empty;
        }
    }
}