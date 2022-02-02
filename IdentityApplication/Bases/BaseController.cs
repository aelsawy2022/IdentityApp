using IdentityApplication.CustomFilters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Bases
{
    [ServiceFilter(typeof(CustomeExceptionFilter))]
    public class BaseController : Controller
    {
    }
}
