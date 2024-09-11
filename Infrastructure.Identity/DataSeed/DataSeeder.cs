using System;
using System.Threading.Tasks;
using Application.Core.Enums;
using Application.Core.Interfaces;
using Application.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Identity.DataSeed
{
    public class DataSeeder(IServiceProvider service) : IDataSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager = service.GetService<UserManager<ApplicationUser>>();
        private readonly RoleManager<IdentityRole> _roleManager = service.GetService<RoleManager<IdentityRole>>();

        public async Task SeedAsync()
        {
            // Seed Role
            foreach (var role in Enum.GetNames(typeof(Role)))
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
                var roles = new[]
                    {Role.Administrator.ToString(), Role.Manager.ToString(), Role.ClassAdmin.ToString()};
                await _userManager.CreateAsync(admin, "Athena@123456");
                await _userManager.AddToRolesAsync(admin, roles);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(admin);
                await _userManager.ConfirmEmailAsync(admin, code);
            }
        }
    }
}
