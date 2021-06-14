using Contracts;
using EForms.API.Core.Services;
using EForms.API.Core.Services.Interfaces;
using EForms.API.Repository.Data.Repositories;
using EForms.API.Repository.Data.Repositories.Interfaces;
using LoggerService;
using Microsoft.Extensions.DependencyInjection;

namespace EForms.API
{
    public static class ServicesConfiguration
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            // Repositories
            services.AddTransient<IFormRepository, FormRepository>();
            services.AddTransient<IAnswerRepository, AnswerRepository>();

            // Services
            services.AddTransient<IFormService, FormService>();

        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
