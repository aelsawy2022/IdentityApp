using SchoolManagement.Models.Models;
using SchoolManagement.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface IManagementService : IBaseService<ManagementModel, ManagementVM>
    {
    }
}
