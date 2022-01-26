using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.domain.Data
{
    public static class SeedRoles
    {
        private static RoleManager<AppRole> _roleManager;

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            var context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<AppDbContext>();
            if ((await context.Database.GetPendingMigrationsAsync()).Any()) await context.Database.MigrateAsync();

            _roleManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<RoleManager<AppRole>>();

            await Create("SuperAdmin");
        }

        private static async Task Create(string name)
        {
            if (!await _roleManager.RoleExistsAsync(name))
            {
                var role = new AppRole { Name = name };
                await _roleManager.CreateAsync(role);
            }
        }
    }
}
