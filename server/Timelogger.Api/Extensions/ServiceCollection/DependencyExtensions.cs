using Microsoft.Extensions.DependencyInjection;
using Timelogger.Application.Extensions;
using Timelogger.Extensions.Infrastructure;

namespace Timelogger.Api.Extensions
{
    public static class DependencyExtensions
    {
        public static void AddTimeLoggerDependency(this IServiceCollection services)
        {
            services.AddInfraDependency();
            services.AddApplicationDependency();
        }
    }
}
