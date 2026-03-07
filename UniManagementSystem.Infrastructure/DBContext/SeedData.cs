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
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Seed Roles
            string[] roles = { "Admin", "Lecturer", "Student" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Seed Admin User
            var adminEmail = "admin@uni.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "System",
                    LastName = "Admin",
                    NationalID = "29901011234567",   // 14-digit, starts with 2 = born 1999
                    Gender = "Male",
                    IsDeleted = false


                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Seed Lecturer User
            var lecturerEmail = "lecturer@uni.com";
            var lecturerUser = await userManager.FindByEmailAsync(lecturerEmail);

            if (lecturerUser == null)
            {
                lecturerUser = new ApplicationUser
                {
                    UserName = lecturerEmail,
                    Email = lecturerEmail,
                    EmailConfirmed = true,
                    FirstName = "Default",
                    LastName = "Lecturer",
                    NationalID = "29801021234567",   
                    Gender = "Male",
                    Salary = 5000,
                    IsDeleted = false
                };

                var result = await userManager.CreateAsync(lecturerUser, "Lecturer@123");

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(lecturerUser, "Lecturer");
            }
        }
    }
}