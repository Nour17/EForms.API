using EForms.API.Infrastructure.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.OpenApi.Models;
using NLog;
using System.IO;
using AutoMapper;
using MappingProfile = EForms.API.Helpers.MappingProfile;

namespace EForms.API
{
    public class Startup
    {
        // fsdfds
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<DbSettings>(options =>
           {
               options.ConnectionString = Configuration.GetSection("EFormDatabaseString:ConnectionString").Value;
               options.Container = Configuration.GetSection("EFormDatabaseString:Container").Value;
               options.Database = Configuration.GetSection("EFormDatabaseString:DatabaseName").Value;
               options.IsContained = Configuration["DOTNET_RUNNING_INCONTAINER"] != null;
           });

            // Custom Repositories and Services
            services.AddCustomServices();

            // Logger
            services.ConfigureLoggerService();

            services.AddCors();

            // Documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EForms API", Version = "v1" });
            });

            // AutoMapper
            #region AutoMapper
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            //app.UseMiddleware<ExceptionHandlerConfiguration>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EForms API V1");
            });

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
