using SchoolManagement.Models.Models;
using SchoolManagement.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface ISchoolService : IBaseService<SchoolModel, SchoolVM>
    {
        Task<List<SchoolModel>> GetAllSchools();
    }
}
