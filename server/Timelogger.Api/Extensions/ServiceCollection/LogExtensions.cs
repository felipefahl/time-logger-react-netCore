using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Timelogger.Api.Extensions
{
    public static class LogExtensions
    {
        public static void AddLog(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()  
                .Enrich.FromLogContext()  
                .WriteTo.Console()
                .WriteTo.Debug()  
                .CreateLogger();  
        }
    }
}