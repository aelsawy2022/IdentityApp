using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface IGovernorateService : IBaseService<GovernorateModel, GovernorateModel>
    {
        Task<List<GovernorateModel>> GetAllGovernorates();
    }
}
