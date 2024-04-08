using BLL.Authorization;
using BLL.Extentions.Automapper;
using BLL.Services.Implement;
using BLL.Services.Interface;
using Manager_Point.ApplicationDbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;

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
            services.AddTransient<IJwtUtils, JwtUtils>();
            services.AddTransient<ISubjectServices, SubjectServices>();
            services.AddTransient<IRoleServices, RoleServices>();
            services.AddTransient<IClassServices, ClassServices>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<ISubject_TeacherServices, Subject_TeacherServices>();
            services.AddTransient<IRole_UserServices, Role_UserServices>();
            services.AddTransient<ICourseServices, CourseServices>();
            services.AddTransient<IMessageServices, MessageServices>();
            services.AddTransient<IGradePointServices, GradePointServices>();
            services.AddTransient<IStudent_ClassServices, Student_ClassServices>();
            services.AddTransient<ITeacher_ClassServices, Teacher_ClassServices>();
            services.AddTransient<IExaminationServices, ExaminationServices>();
            services.AddTransient<IAcademicPerformanceServices, AcademicPerformanceServices>();
            #endregion

            #region ================== DI Mapper ==================
            services.AddAutoMapper(typeof(Subject_Mapping).Assembly);
            services.AddAutoMapper(typeof(Role_Mapping).Assembly);
            services.AddAutoMapper(typeof(Class_Mapping).Assembly);
            services.AddAutoMapper(typeof(Course_Mapping).Assembly);
            services.AddAutoMapper(typeof(GradePoint_Mapping).Assembly);
            services.AddAutoMapper(typeof(Message_Mapping).Assembly);
            services.AddAutoMapper(typeof(User_Mapping).Assembly);
            services.AddAutoMapper(typeof(Subject_Teacher_Mapping).Assembly);
            services.AddAutoMapper(typeof(Role_User_Mapping).Assembly);
            services.AddAutoMapper(typeof(Examination_Mapping).Assembly);
            services.AddAutoMapper(typeof(AcademicPerformances_Mapping).Assembly);
            #endregion

            return services;
        }
    }
}
