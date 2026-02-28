
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniManagementSystem.Domain.Models;

namespace UniManagementSystem.Infrastructure.DBContext
{
    public static class SeedData
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>>();

            string[] Roles = { "Admin", "Lecturer", "Student" };

            foreach (var role in Roles)
            {
                if(! await roleManager.RoleExistsAsync(role) ) 
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
