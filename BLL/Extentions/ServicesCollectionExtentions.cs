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
            services.AddTransient<IClassServices, ClassServices>();
            services.AddTransient<ICourseService, CourseServices>();
            services.AddTransient<IMessageService, MessagesService>();
            services.AddTransient<IGradePointService, GradePointService>();
            #endregion

            #region ================== DI Mapper ==================
            services.AddAutoMapper(typeof(Subject_Mapping).Assembly);
            services.AddAutoMapper(typeof(Role_Mapping).Assembly);
            services.AddAutoMapper(typeof(Class_Mapping).Assembly);
            services.AddAutoMapper(typeof(Course_Mapping).Assembly);
            services.AddAutoMapper(typeof(GradePoint_Mapping).Assembly);
            services.AddAutoMapper(typeof(Message_Mapping).Assembly);
            #endregion

            return services;
        }
    }
}
