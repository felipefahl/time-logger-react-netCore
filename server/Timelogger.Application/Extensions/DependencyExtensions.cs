using Microsoft.Extensions.DependencyInjection;
using Timelogger.Application.Services;
using Timelogger.Interfaces.Services;
namespace Timelogger.Application.Extensions
{
    public static class DependencyExtensions
    {
        public static void AddApplicationDependency(this IServiceCollection services)
        {
            services.AddScoped<IProjectService, ProjectService>();
        }
    }
}
