using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Infrastructure.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers.API
{
    [ApiController]
    [ServiceFilter(typeof(CustomeExceptionFilter))]
    public class BaseApiController : ControllerBase
    {
    }
}
