

namespace LMS.Data
{
    public static class ModuleDataDependencies
    {
        public static IServiceCollection AddServicesData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbcontext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConections"));
            });
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;

        }

    }
}
