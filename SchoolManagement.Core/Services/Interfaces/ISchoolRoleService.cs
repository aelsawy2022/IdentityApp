using SchoolManagement.Models.Models;
using SchoolManagement.Models.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface ISchoolRoleService : IBaseService<RolesModel, RoleViewModel>
    {
        Task<bool> ActivateRole(Guid roleId);
    }
}
