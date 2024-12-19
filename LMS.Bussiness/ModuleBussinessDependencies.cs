using FluentValidation;
using FluentValidation.AspNetCore;
using LMS.Bussiness.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LMS.Bussiness
{
    public static class ModuleBussinessDependencies
    {

        public static IServiceCollection AddLMSBussinessServices(this IServiceCollection services)
        {
            //Service For FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation()
                 .AddFluentValidationClientsideAdapters();

            // Service for Confirm Email 
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext!);
            });


            services.AddScoped<IAuthenticationService, AuthenticationService>();
            // services.AddScoped<ICertificationService, CertificationService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
