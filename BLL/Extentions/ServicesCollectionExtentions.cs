using BLL.Extentions.Automapper;
using BLL.Services.Implement;
using BLL.Services.Interface;
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

            #region ================== DI Services ==================
            services.AddTransient<ISubjectServices, SubjectServices>();
            services.AddTransient<IRoleServices, RoleServices>();
            #endregion

            #region ================== DI Mapper ==================
            services.AddAutoMapper(typeof(Subject_Mapping).Assembly);
            services.AddAutoMapper(typeof(Role_Mapping).Assembly);
            #endregion

            return services;
        }
    }
}
