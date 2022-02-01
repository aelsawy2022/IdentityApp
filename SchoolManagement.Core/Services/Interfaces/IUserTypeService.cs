using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface IUserTypeService : IBaseService<UserTypeModel, UserTypeModel>
    {
        Task<bool> ActivateType(Guid typeId);
        Task<List<UserTypeModel>> GetAllUserTypes();
    }
}
