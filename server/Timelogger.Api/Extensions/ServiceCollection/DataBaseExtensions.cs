using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Timelogger.Entities;
using Timelogger.Infrastructure;

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
                Id = Guid.Parse("47ceaf09-1b33-4425-a7f4-931d8d2a6cb5"),
                Name = "e-conomic Interview",
                DeadLine = DateTime.UtcNow.AddDays(5),
                ClosedAt = DateTime.UtcNow.AddDays(-1)
            };

            var testProject2 = new Project
            {
                Id = Guid.Parse("794fd433-9347-4ce4-a999-14d115bd0dbe"),
                Name = "personal Data",
                DeadLine = DateTime.UtcNow.AddDays(-9),
            };

            var testProject3 = new Project
            {
                Id = Guid.Parse("af4a717b-fa7d-44cb-80a0-9fb59c14bdb8"),
                Name = "family business",
                DeadLine = DateTime.UtcNow.AddDays(15),
            };

            var testProject4 = new Project
            {
                Id = Guid.Parse("b9bfedfc-bc35-4c96-86b2-845df714d1d4"),
                Name = "free lancer",
                DeadLine = DateTime.UtcNow.AddDays(35),
            };

            context.Projects.Add(testProject1);
            context.Projects.Add(testProject2);
            context.Projects.Add(testProject3);
            context.Projects.Add(testProject4);

            context.SaveChanges();
        }

    }
}
