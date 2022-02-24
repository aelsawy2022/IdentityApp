using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Infrastructure.CustomFilters;

namespace IdentityApplication.Bases
{
    [Authorize]
    [ServiceFilter(typeof(CustomeExceptionFilter))]
    [ServiceFilter(typeof(ActivityLoggingFilter))]
    public class BaseController : Controller
    {
    }
}
