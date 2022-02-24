using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Infrastructure.CustomFilters;
using SchoolManagement.Models.Models;
using SchoolManagement.ViewModels.ViewModels;
using System;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(CustomeExceptionFilter))]
    public class ErrorLogsController : Controller
    {
        private readonly IErrorLogService _errorLogService;

        public ErrorLogsController(IErrorLogService errorLogService)
        {
            _errorLogService = errorLogService;
        }

        [Authorize(Roles.SUPER_ADMIN)]
        public async Task<IActionResult> Index(
            DateTime? created = null,
            int currentPage = 1, int maxRows = 10,
            string searchBtn = null
            )
        {
            ErrorLogVM errorLogVM = new ErrorLogVM();
            errorLogVM.ErrorLogFilter = new ErrorLogFilter();
            errorLogVM.ErrorLogFilter.TimeStamp = created;
            errorLogVM.ErrorLogFilter.CurrentPage = currentPage;
            if (searchBtn != null && searchBtn == "Search")
            {
                errorLogVM.ErrorLogFilter.CurrentPage = 1;
            }
            errorLogVM.ErrorLogFilter.MaxRows = maxRows;

            errorLogVM.ErrorLogs = await _errorLogService.GetLogs(errorLogVM.ErrorLogFilter);
            return View(errorLogVM);
        }
    }
}
