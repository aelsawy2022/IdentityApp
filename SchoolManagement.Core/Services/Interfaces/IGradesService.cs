using SchoolManagement.Models.Models;
using SchoolManagement.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface IGradesService : IBaseService<GradeModel, GradeVM>
    {
        Task<SchoolModel> GetGradeSchool(object Id);
    }
}
