using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Infrastructure.CustomFilters
{
    public class CustomeExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<CustomeExceptionFilter> _logger;

        public CustomeExceptionFilter(IWebHostEnvironment hostEnvironment, ILogger<CustomeExceptionFilter> logger)
        {
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            bool isApiRequest = context.HttpContext.Request.Path.Value.StartsWith("/api");
            var ex = context.Exception;
            while (ex.InnerException != null) ex = ex.InnerException;

            string LogMessage = string.Empty;
            string Controller = context.RouteData.Values["controller"].ToString();
            string Action = context.RouteData.Values["action"].ToString();
            string Version = context.RouteData.Values["version"]?.ToString();
            string ResponseMessage = string.Empty;

            string UserName = string.Empty;
            var currentUser = context.HttpContext.User;
            //if (currentUser != null && currentUser.Claims != null && currentUser.Claims.Any() && currentUser.Claims.Where(c => c.Type.Contains("USERNAME")) != null)
            //    UserName = currentUser.Claims.Where(c => c.Type.Contains("USERNAME")).FirstOrDefault().Value;

            if (currentUser != null && currentUser.Identity != null && currentUser.Identity.Name != null)
                UserName = currentUser.Identity.Name;

            string QueryString = context.HttpContext.Request.QueryString.HasValue ? context.HttpContext.Request.QueryString.Value : "";
            string url = context.HttpContext.Request.Scheme + "://" + context.HttpContext.Request.Host.Value + context.HttpContext.Request.Path.Value + QueryString;

            if (ex is UnauthorizedAccessException)
            {
                LogMessage = string.Format("[Version: {0}] [User: {1}] [Controller: {2}] [Action: {3}]: [Url: {4}] [Exception: Unauthorized Access Exception] \n \n", Version, UserName, Controller, Action, url);
                context.HttpContext.Response.StatusCode = 401;
            }
            else
            {
                ResponseMessage = ex.ToString();
                LogMessage = string.Format("[Version: {0}] [User: {1}] [Controller: {2}] [Action: {3}] [Url: {4}] \n [Exception: {5}] \n \n", Version, UserName, Controller, Action, url, ex.ToString());
                context.HttpContext.Response.StatusCode = 500;
            }

            _logger.LogError(ex, LogMessage);

            dynamic result;
            if (!isApiRequest)
            {
                result = new ViewResult { ViewName = "Error" };
                var modelMetadata = new EmptyModelMetadataProvider();
                result.ViewData = new ViewDataDictionary(modelMetadata, context.ModelState);

                if (_hostEnvironment.IsDevelopment())
                {
                    result.ViewData.Add("ErrorMsg", ResponseMessage);
                }
                else
                {
                    result.ViewData.Add("ErrorMsg", "An unexpected fault happened. Try again later");
                }
            }
            else
            {
                if (_hostEnvironment.IsDevelopment())
                {
                    result = new JsonResult(new { error = ResponseMessage });
                }
                else
                {
                    result = new JsonResult(new { error = "An unexpected fault happened. Try again later" });
                }
            }

            context.Result = result;
            context.ExceptionHandled = true;

            base.OnException(context);
        }
    }
}
