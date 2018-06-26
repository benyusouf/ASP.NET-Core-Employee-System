using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SmartTech.Configuration
{
    public class ApplicationUserRoleSeed
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationUserRoleSeed(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        
        // Method to add role to AspNet Roles table if it doesn't exist on app startup
        public async Task Seed()
        {
            if( (await _roleManager.FindByNameAsync("Employee")) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Employee"});
            }
            
            if( (await _roleManager.FindByNameAsync("Admin")) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin"});
            }
        }
    }
}