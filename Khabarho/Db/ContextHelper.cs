using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Khabarho.Models;
using Microsoft.AspNetCore.Identity;

namespace Khabarho.Db
{
    public static class ContextHelper
    {
        public static async Task Seeding(DataContext db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any(r => r.NormalizedName.Equals("ADMIN")))
            {
                var adminRole = new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };
                
                await roleManager.CreateAsync(adminRole);
            }

            if (!userManager.Users.Any(u => u.NormalizedUserName.Equals("ADMIN")))
            {
                var adminUser = new User
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123");

                if (result.Succeeded)
                {
                    var role = await roleManager.FindByNameAsync("Admin");

                    await userManager.AddToRoleAsync(await userManager.FindByNameAsync("admin"), role.Name);
                }
            }

            if (!db.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category {Id = Guid.NewGuid(), Title = "Разработка"},
                    new Category {Id = Guid.NewGuid(), Title = "Администрирование"},
                    new Category {Id = Guid.NewGuid(), Title = "Дизайн"},
                    new Category {Id = Guid.NewGuid(), Title = "Менеджмент"},
                    new Category {Id = Guid.NewGuid(), Title = "Маркетинг"},
                    new Category {Id = Guid.NewGuid(), Title = "Научпоп"},
                };

                await db.Categories.AddRangeAsync(categories);
                await db.SaveChangesAsync();
            }

            if (!db.Types.Any())
            {
                var types = new List<Models.Type>
                {
                    new Models.Type {Id = Guid.NewGuid(), Title = "Статьи"},
                    new Models.Type {Id = Guid.NewGuid(), Title = "Новости"},
                };

                await db.Types.AddRangeAsync(types);
                await db.SaveChangesAsync();
            }
        }
    }
}