using Microsoft.Extensions.DependencyInjection;
using Timelogger.Infrastructure.Repositories;
using Timelogger.Interfaces.Repositories;

namespace Timelogger.Extensions.Infrastructure
{
    public static class DependencyExtensions
    {
        public static void AddInfraDependency(this IServiceCollection services)
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITimelogRepository, TimelogRepository>();
        }
    }
}
