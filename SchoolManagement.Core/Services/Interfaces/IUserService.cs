using SchoolManagement.Models.Models;
using SchoolManagement.Models.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface IUserService : IBaseService<UsersViewModel, UsersViewModel>
    {
        Task<bool> AddToAdminRole(string userName, bool isAdmin);
        Task<bool> AddToSuperAdminRole(string userName, bool isSuperAdmin);
        Task<UserServiceResponse> CreateUser(UsersViewModel usersViewModel);
        Task<UsersViewModel> GetUsers(int currentPage, int maxRows);
    }
}
