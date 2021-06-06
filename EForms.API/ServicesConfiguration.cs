using Contracts;
using EForms.API.Core.Services;
using EForms.API.Core.Services.Interfaces;
using EForms.API.Repository.Data.Repositories;
using EForms.API.Repository.Data.Repositories.Interfaces;
using LoggerService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API
{
    public static class ServicesConfiguration
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            // Repositories
            services.AddTransient<IFormRepository, FormRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();

            // Services
            services.AddTransient<IContainerService, ContainerService>();
            services.AddTransient<IFormService, FormService>();
            services.AddTransient<ISectionService, SectionService>();
            services.AddTransient<IQuestionService, QuestionService>();
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
