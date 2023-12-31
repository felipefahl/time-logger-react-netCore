﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Timelogger.Api.Extensions;
using Timelogger.Api.Extensions.ServiceCollection;

namespace Timelogger.Api
{
	public class Startup
	{
		private readonly IWebHostEnvironment _environment;
		public IConfigurationRoot Configuration { get; }

		public Startup(IWebHostEnvironment env)
		{
			_environment = env;

			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{			
			services.AddLog();

			services.AddSwagger();

			services.AddDataBase();
			services.AddTimeLoggerDependency();

			services.AddMvc(options => options.EnableEndpointRouting = false).AddJsonOptions(options => {
				options.JsonSerializerOptions.IgnoreNullValues = true;
			});

			services.AddVersioning();

			if (_environment.IsDevelopment())
			{
				services.AddCors();
			}
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseCustomException();

			if (env.IsDevelopment())
			{
				app.UseCors(builder => builder
					.AllowAnyMethod()
					.AllowAnyHeader()
					.SetIsOriginAllowed(origin => true)
					.AllowCredentials());
			}

			app.UseMvc();
			app.UseSwaggerSetup();

			app.SeedDataBase();
			
		}
	}
}