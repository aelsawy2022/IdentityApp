using SchoolManagement.Models.Models;
using SchoolManagement.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface IClassService : IBaseService<ClassModel, ClassVM>
    {
        Task<List<ClassModel>> GetByGradeId(Guid gradeId);
    }
}
