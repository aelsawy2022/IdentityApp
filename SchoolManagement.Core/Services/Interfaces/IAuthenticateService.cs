using Microsoft.AspNetCore.Identity;
using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface IAuthenticateService
    {
        Task<SignInResult> Authenticate(LoginModel loginModel);
        IEnumerable<Claim> GenerateClaims(UsersModel userModel);
    }
}
