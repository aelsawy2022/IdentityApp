using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.Persistance.Data.Entities;
using SchoolManagement.Persistance.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Core.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AuthenticateService> _logger;
        private readonly ITokenService _tokenService;

        public AuthenticateService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            SignInManager<User> signInManager,
            ILogger<AuthenticateService> logger,
            ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _signInManager = signInManager;
            _logger = logger;
            _tokenService = tokenService;
        }

        public async Task<SignInResult> Authenticate(LoginModel login)
        {
            login.Email = login.Email.Trim();
            login.Password = login.Password.Trim();

            return await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, lockoutOnFailure: false);
        }

        public IEnumerable<Claim> GenerateClaims(UsersModel userModel)
        {
            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userModel.Name ),
                            new Claim("USERID", userModel.Id.ToString()),
                            new Claim("EMAIL", userModel.Email)
                            //new Claim(ClaimTypes.Role, adminDTO.AdminRole)
            };

            return claims;
        }
    }
}
