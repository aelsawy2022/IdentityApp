using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SchoolManagement.Core.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
    }
}
