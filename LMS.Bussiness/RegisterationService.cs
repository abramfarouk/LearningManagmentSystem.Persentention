using LMS.Data.Data;
using LMS.Data.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Bussiness
{
    public static class RegisterationService
    {
        public static IServiceCollection AddRegisterationService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddIdentity<User, Role>(opt =>
            {
                //Password
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 6;
                opt.Password.RequiredUniqueChars = 1;
                // Lockout 
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.AllowedForNewUsers = true;

                //User


                opt.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;


            }).AddEntityFrameworkStores<ApplicationDbcontext>().AddDefaultTokenProviders(); ;



            var emailSettings = new EmailSettings();
            configuration.GetSection(nameof(emailSettings)).Bind(emailSettings);
            services.AddSingleton(emailSettings);

            return services;

        }

    }

}
