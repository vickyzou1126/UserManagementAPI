using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using UserManagementAPI.Configs;
using UserManagementAPI.Constants;
using UserManagementAPI.Exceptions;

namespace UserManagementAPI.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, IConfiguration configuration)
        {
            var path = httpContext.Request.Path;
            if (path.HasValue && path.Value.StartsWith("/api/users"))
            {
                var headers = httpContext.Request.Headers;
                if (!headers.ContainsKey(Constant.RequestToken))
                    throw new UnauthorizedException(ErrorCodes.RequestUnauthorized, DefaultErrorDetails.DefaultUnauthorizedMsg);

                var authConfig = configuration.GetSection("ApiAuth").Get<AuthOptions>();
                if (headers.TryGetValue(Constant.RequestToken,
                out var requestToken) && (string.IsNullOrWhiteSpace(requestToken) || !authConfig.Token.Equals(requestToken)))
                {
                    throw new UnauthorizedException(ErrorCodes.RequestUnauthorized, DefaultErrorDetails.DefaultUnauthorizedMsg);
                }
            }
            return _next(httpContext);
        }
    }
}
