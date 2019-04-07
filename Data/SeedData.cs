using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.EntityFrameworkCore.Internal;
using ejemplo.Model;
using ejemplo.src.Authorization;

namespace ejemplo.Data
{
    public static class SeedData {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                var appSettings = serviceProvider.GetRequiredService<IConfiguration>();

                context.Database.EnsureCreated();

                if (context.Maestro.Any()) {
                    Console.Write("DB Ready");
                    return;
                }

                /**-------------------Maestro ------------------------**/

                var prueba = new Maestro()
                {
                    Titulo = "Dr.",
                    Nombre = "prueba",
                    Apellido = "prueba1"
                };
                prueba.StringIdentifier = (prueba.Nombre);
                context.Maestro.Add(prueba);
                context.SaveChanges();

                /**-------------------Admin ------------------------**/
                var admin = new ApplicationUser()
                {
                    UserName = appSettings["adminuser:UserName"],
                    Nombre = appSettings["adminuser:Name"],
                    Email = appSettings["adminuser:UserName"],
                    Apellido = appSettings["adminuser:LastName"],
                    StringIdentifier = appSettings["adminuser:StringIdentifier"]
                };

                var adminId = EnsureUser(serviceProvider, admin, appSettings["adminuser:Pass"]).Result;
                EnsureRole(serviceProvider, adminId, Constants.AdministratorsRole).Wait();
                context.SaveChanges();

                CreateRole(serviceProvider).Wait();

            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, ApplicationUser newuser, string password)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByNameAsync(newuser.UserName);
            if (user == null)
            {
                user = newuser;
                await userManager.CreateAsync(user, password);
                await userManager.ConfirmEmailAsync(user, await userManager.GenerateEmailConfirmationTokenAsync(user));
            }
            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));

            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByIdAsync(uid);
            IR = await userManager.AddToRoleAsync(user, role);
            return IR;
        }

        private static async Task<IdentityResult> CreateRole(IServiceProvider serviceProvider)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var rolesname = new String[] { $"{Constants.ConsultorRole}", $"{Constants.EditorRole}" };
            foreach (var rolename in rolesname)
            {
                IR = await roleManager.CreateAsync(new IdentityRole(rolename));
            }
            return IR;
        }
    }
}
