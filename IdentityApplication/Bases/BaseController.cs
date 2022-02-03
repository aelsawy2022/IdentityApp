using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Infrastructure.CustomFilters;

namespace IdentityApplication.Bases
{
    [Authorize]
    [ServiceFilter(typeof(CustomeExceptionFilter))]
    public class BaseController : Controller
    {
    }
}
