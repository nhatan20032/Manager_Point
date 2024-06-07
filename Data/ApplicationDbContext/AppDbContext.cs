using Data.Configuration;
using Data.Models;
using Manager_Point.Configuration;
using Manager_Point.Models;
using Microsoft.EntityFrameworkCore;

namespace Manager_Point.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        #region ================= Database Set =================
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<AcademicPerformance> AcademicPerformances { get; set; }
        public virtual DbSet<Examination> Examinations { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<GradePoint> GradePoints { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Student_Class> Students_Classes { get; set; }
        public virtual DbSet<Teacher_Class> Teacher_Classes { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Subject_Teacher> Subjects_Teachers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<User_Role> Users_Roles { get; set; }
        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                //optionsBuilder.UseSqlServer("Data Source=LAPTOP-RST0N7P9; Initial Catalog = manager_point; Integrated Security = True; Encrypt = True; TrustServerCertificate = True;");
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-PFT30L7; Initial Catalog = manager_point; Integrated Security = True; Encrypt = True; TrustServerCertificate = True;");

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region ================= Configuration =================
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AcademicPerformConfiguration());
            modelBuilder.ApplyConfiguration(new ClassConfiguration());
            modelBuilder.ApplyConfiguration(new ExaminationConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new GradePointConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new Student_ClassConfiguration());
            modelBuilder.ApplyConfiguration(new Subject_TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
            modelBuilder.ApplyConfiguration(new Teacher_ClassConfiguration());
            modelBuilder.ApplyConfiguration(new User_RoleConfiguration());
            #endregion
        }
    }
}
