using IdentityApplication.Middlewares;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;

namespace IdentityApplication.ExtensionMethods
{
    public static class AuthenticationOverrideMiddlewareExtension
    {
        public static IApplicationBuilder UseAuthenticationOverride(this IApplicationBuilder app, string defaultScheme)
        {
            return app.UseMiddleware<AuthenticationOverrideMiddleware>(new AuthenticationOptions { DefaultScheme = defaultScheme });
        }
        public static IApplicationBuilder UseAuthenticationOverride(this IApplicationBuilder app, AuthenticationOptions authenticationOptionsOverride)
        {
            return app.UseMiddleware<AuthenticationOverrideMiddleware>(authenticationOptionsOverride);
        }
    }
}
