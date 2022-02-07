using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IdentityApplication.Middlewares
{
    public class AuthenticationOverrideMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuthenticationOptions _authenticationOptionsOverride;

        public AuthenticationOverrideMiddleware(RequestDelegate next, AuthenticationOptions authenticationOptionsOverride)
        {
            _next = next;
            _authenticationOptionsOverride = authenticationOptionsOverride;
        }
        public async Task Invoke(HttpContext context)
        {
            // Add overriden options to HttpContext
            context.Features.Set(_authenticationOptionsOverride);
            await _next(context);
        }
    }
}
