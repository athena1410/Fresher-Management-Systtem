using System;
using System.Threading.Tasks;
using Application.Core.Enums;
using Application.Core.Interfaces;
using Application.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Identity.DataSeed
{
    public class DataSeeder : IDataSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataSeeder(IServiceProvider service)
        {
            _userManager = service.GetService<UserManager<ApplicationUser>>();
            _roleManager = service.GetService<RoleManager<IdentityRole>>(); 
        }

        public async Task SeedAsync()
        {
            // Seed Roles
            foreach (var role in Enum.GetNames(typeof(Roles)))
            {
                if (await _roleManager.RoleExistsAsync(role))
                {
                    continue;
                }
                await _roleManager.CreateAsync(new IdentityRole(role));
            }


            // Seed User

            ApplicationUser admin = await _userManager.FindByNameAsync("athena1410");
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = "athena1410",
                    Email = "duytoan.mk@gmail.com",
                    FirstName = "Toan",
                    LastName = "Ha"
                };
                await _userManager.CreateAsync(admin, "Yennhi@123456");
                await _userManager.AddToRoleAsync(admin, Roles.Administrator.ToString());
            }
        }
    }
}
