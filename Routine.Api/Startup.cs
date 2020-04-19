using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Routine.Api.Data;
using Routine.Api.Services;

namespace Routine.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers(setup => { setup.ReturnHttpNotAcceptable = true; })
				.AddXmlDataContractSerializerFormatters()
				.ConfigureApiBehaviorOptions(setup =>
				{
					setup.InvalidModelStateResponseFactory = context =>
					{
						var problemDetails = new ValidationProblemDetails(context.ModelState)
						{
							Type = "http://www.baidu.com",
							Title = "Error",
							Status = StatusCodes.Status422UnprocessableEntity,
							Detail = "RTFM",
							Instance = context.HttpContext.Request.Path
						};

						problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

						return new UnprocessableEntityObjectResult(problemDetails)
						{
							ContentTypes = {"application/problem+json"}
						};
					};
				});

			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			
			services.AddScoped<ICompanyRepository, CompanyRepository>();

			services.AddDbContext<RoutineDbContext>(option =>
			{
				option.UseSqlite("Data Source=routine.db");
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler(appBuilder =>
				{
					appBuilder.Run(async context =>
					{
						context.Response.StatusCode = 500;
						await context.Response.WriteAsync("Unexpected Error!");
					});
				});
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
