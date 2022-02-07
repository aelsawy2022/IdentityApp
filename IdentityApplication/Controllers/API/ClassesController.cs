using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Infrastructure.CustomFilters;
using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers.API
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClassesController : BaseApiController
    {
        private readonly IClassService _classService;

        public ClassesController(
            IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet("{gradeId}", Name = "GetClasses")]
        [Authorize(Roles.ADMIN, Roles.SCHOOL_ADMIN)]
        public async Task<IActionResult> GetClasses(Guid gradeId)
        {
            try
            {
                throw new NotImplementedException();
                List<ClassModel> classes = await _classService.GetByGradeId(gradeId);
                if(classes != null)
                {
                    return Ok(classes);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
