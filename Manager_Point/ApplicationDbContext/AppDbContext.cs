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
        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=ADMIN-PC; Initial Catalog = manager_point; Integrated Security = True; Encrypt = True; TrustServerCertificate = True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region ================= Configuration =================
            #endregion
        }
    }
}
