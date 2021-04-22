using EForms.API.Core.Services;
using EForms.API.Core.Services.Interfaces;
using EForms.API.Repository.Data.Repositories;
using EForms.API.Repository.Data.Repositories.Interfaces;
using EForms.API.Infrastructure.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace EForms.API
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
            services.AddControllers();

            services.Configure<DbSettings>(options =>
           {
               options.ConnectionString = Configuration.GetSection("EFormDatabaseString:ConnectionString").Value;
               options.Database = Configuration.GetSection("EFormDatabaseString:DatabaseName").Value;
           });

            // Repositories
            services.AddTransient<IFormRepository, FormRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();

            // Services
            services.AddTransient<IContainerService, ContainerService>();
            services.AddTransient<IFormService, FormService>();
            services.AddTransient<ISectionService, SectionService>();
            services.AddTransient<IQuestionService, QuestionService>();

            services.AddCors();

            // Documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EForms API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

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
