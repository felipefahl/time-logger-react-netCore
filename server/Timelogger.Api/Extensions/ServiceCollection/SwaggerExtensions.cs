using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace Timelogger.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Timelogger API",
                    Description = "A API to register your timelog spent on ptojects",
                    Contact = new OpenApiContact
                    {
                        Name = "Felipe Fahl Nicolau",
                        Email = "ffn2001@gmail.com",
                        Url = new Uri("https://github.com/felipefahl"),
                    },
                });
            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
		{
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = string.Empty;
            });
		}

    }
}