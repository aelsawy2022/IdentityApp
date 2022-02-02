using SchoolManagement.Models.Models;
using SchoolManagement.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface IUserService : IBaseService<UserVM, UserVM>
    {
        Task<bool> AddToAdminRole(string userName, bool isAdmin);
        Task<bool> AddToSuperAdminRole(string userName, bool isSuperAdmin);
        Task<UserServiceResponse> CreateUser(UserVM usersViewModel);
        Task<UserVM> GetUsers(int currentPage, int maxRows);
    }
}
