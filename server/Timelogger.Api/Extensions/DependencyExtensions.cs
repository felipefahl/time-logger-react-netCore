using Microsoft.Extensions.DependencyInjection;

namespace Timelogger.Api.Extensions
{
    public static class DependencyExtensions
    {
        public static void AddTimeLoggerDependency(this IServiceCollection services)
        {
            //services.AddInfraDependency();
            //services.AddApplicationDependency();
        }

    }
}
