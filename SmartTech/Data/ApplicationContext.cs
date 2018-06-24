using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartTech.Data.Entities;

namespace SmartTech.Data
{
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext()
        {
            
        }
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
          optionsBuilder.UseSqlServer(@"Data Source=abdullahi\SQLEXPRESS;Database=SmartTech;Trusted_Connection=True;MultipleActiveResultSets=True");
        
        }
        
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        
    }
}