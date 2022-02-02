using SchoolManagement.Models.Models;
using SchoolManagement.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface IClassUserService : IBaseService<ClassUsersModel, ClassUserVM>
    {
        Task<ClassUserServiceResponse> AddUsers(ClassUserVM viewModel);
        Task<ClassUserServiceResponse> DeleteUsers(ClassUserVM viewModel);
    }
}
