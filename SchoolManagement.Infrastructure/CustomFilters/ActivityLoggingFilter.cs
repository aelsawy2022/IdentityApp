using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SchoolManagement.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.CustomFilters
{
    public class ActivityLoggingFilter : IActionFilter
    {
        private readonly IActivityLogService _activityLogService;

        public ActivityLoggingFilter(IActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;

            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();

            //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            //...Then we copy the entire request stream into the new buffer.
            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            //We convert the byte[] into a string using UTF8 encoding...
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            //..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return $"{response.StatusCode}: {text}";
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string data = "";

            var routeData = context.RouteData;
            string controller = routeData.Values["controller"].ToString();
            string action = routeData.Values["action"].ToString();

            string QueryString = context.HttpContext.Request.QueryString.HasValue ? context.HttpContext.Request.QueryString.Value : "";
            var url = context.HttpContext.Request.Scheme + "://" + context.HttpContext.Request.Host.Value + context.HttpContext.Request.Path.Value + QueryString;

            if (!string.IsNullOrEmpty(context.HttpContext.Request.QueryString.Value))
            {
                data = context.HttpContext.Request.QueryString.Value;
            }
            else
            {
                var arguments = context.ActionArguments;

                var value = arguments.FirstOrDefault().Value;

                var convertedValue = JsonConvert.SerializeObject(value);
                data = convertedValue;
            }

            var user = context.HttpContext.User.Identity.Name;

            var ipAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();

            _activityLogService.Log(new Models.Models.ActivityLogModel()
            {
                Controller = controller,
                Action = action,
                CreationDate = DateTime.Now,
                Url = url,
                Username = user,
                IPAddress = ipAddress,
                Request = data,
            });
        }
    }
}
