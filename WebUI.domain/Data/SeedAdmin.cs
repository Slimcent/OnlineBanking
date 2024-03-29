﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineBanking.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.domain.Data
{
    public static class SeedAdmin
    {
        private const string AdminUser = "Admin";
        private const string AdminPassword = "Secret@123$";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            var context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<AppDbContext>();
            if ((await context.Database.GetPendingMigrationsAsync()).Any()) await context.Database.MigrateAsync();

            var userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<User>>();

            var user = await userManager.FindByNameAsync(AdminUser);
            if (user != null) return;
            user = new User
            { Email = "admin@domain.com", UserName = "Admin", PhoneNumber = "090-123-4090", EmailConfirmed = true };
            var createUser = await userManager.CreateAsync(user, AdminPassword);

            if (createUser.Succeeded) await userManager.AddToRoleAsync(user, "SuperAdmin");
        }
    }
}
