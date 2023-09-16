using Microsoft.AspNetCore.Builder;
using Timelogger.Api.Middlewares;

namespace Timelogger.Api.Extensions.ServiceCollection
{
    public static class MiddlewareExtensions
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseMiddleware<ConfigureExceptionMiddleware>();
        }
    }
}
