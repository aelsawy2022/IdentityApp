using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Core.Services.Interfaces;
using SchoolManagement.Models.Models;
using SchoolManagement.Persistance.Data.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityApplication.Controllers.API
{
    [Route("api/[controller]")]
    public class LoginsController : BaseApiController
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IUserService _userService;
        private readonly ILogger<LoginsController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;

        public LoginsController(
            IAuthenticateService authenticateService,
            IUserService userService,
            ITokenService tokenService,
            ILogger<LoginsController> logger, IConfiguration config, UserManager<User> userManager)
        {
            _authenticateService = authenticateService;
            _userService = userService;
            _tokenService = tokenService;
            _logger = logger;
            _config = config;
            _userManager = userManager;
        }

        [HttpPost("")]
        public async Task<IActionResult> Authenticate(LoginModel loginModel)
        {
            try
            {
                var result = await _authenticateService.Authenticate(loginModel);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    //var user = await _userService.GetByEmail(loginModel.Email);
                    var user = await _userManager.FindByEmailAsync(loginModel.Email);
                    string token = _tokenService.GenerateToken(_authenticateService.GenerateClaims(new UsersModel() { Name = user.UserName, Id = user.Id, Email = user.Email }));
                    string refreshToken = _tokenService.GenerateRefreshToken();

                    _ = int.TryParse(_config["Jwt:RefreshTokenLifetimeInMinutes"], out int refreshTokenLifetimeInMinutes);

                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenLifetimeInMinutes);

                    await _userManager.UpdateAsync(user);
                    //await _userService.Update(user);

                    return Ok(new { user = user, token = token, refreshToken = refreshToken });
                }
                throw new UnauthorizedAccessException("Entered Email or Password is wrong");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            try
            {
                if (tokenModel is null)
                {
                    return BadRequest("Invalid client request");
                }

                string? accessToken = tokenModel.AccessToken;
                string? refreshToken = tokenModel.RefreshToken;

                var principal = GetPrincipalFromExpiredToken(accessToken);
                if (principal == null)
                {
                    return BadRequest("Invalid access token or refresh token");
                }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                string username = principal.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                var user = await _userManager.FindByNameAsync(username);

                if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    return BadRequest("Invalid access token or refresh token, Please login again");
                }

                var newAccessToken = _tokenService.GenerateToken(principal.Claims.ToList());
                var newRefreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                await _userManager.UpdateAsync(user);

                return Ok(new { token = newAccessToken, refreshToken = newRefreshToken });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken)
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
    }
}
