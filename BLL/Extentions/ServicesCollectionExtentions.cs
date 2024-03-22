using BLL.Services.RoleServices.Impliment;
using BLL.Services.RoleServices.Interface;
using BLL.Services.SubjectServices.Implement;
using BLL.Services.SubjectServices.Interface;
using Manager_Point.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Extentions
{
    public static class ServicesCollectionExtentions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<AppDbContext>(options =>
            {
                // Configure your DbContext options here
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            services.AddTransient<IRoleServices, RoleServices>();
            services.AddTransient<ISubjectServices, SubjectServices>();

            return services;
        }
    }
}
