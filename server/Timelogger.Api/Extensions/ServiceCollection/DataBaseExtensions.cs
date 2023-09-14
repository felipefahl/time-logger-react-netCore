using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Timelogger.Entities;

namespace Timelogger.Api.Extensions
{
    public static class DataBaseExtensions
	{
		public static void AddDataBase(this IServiceCollection services)
		{
			services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("e-conomic interview"));
		}

		public static void SeedDataBase(this IApplicationBuilder app)
		{
			var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
			using (var scope = serviceScopeFactory.CreateScope())
			{
				SeedDatabase(scope);
			}
		}

		private static void SeedDatabase(IServiceScope scope)
		{
			var context = scope.ServiceProvider.GetService<ApiContext>();
			var testProject1 = new Project
			{
				Id = System.Guid.Empty,
				Name = "e-conomic Interview"
			};

			context.Projects.Add(testProject1);

			context.SaveChanges();
		}

	}
}
