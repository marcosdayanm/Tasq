using System;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Net;
using Tasq.Data;
using Tasq.Models;
using System.Threading.Tasks;

namespace Tasq.Data
{
    public class Seed
    {

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));





                // Admin Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();


                var adminUser = await userManager.FindByEmailAsync("marcos@tasq.com");
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        Nombre = "Marcos",
                        UserName = "marcos@tasq.com",
                        Email = "marcos@tasq.com",
                        EmailConfirmed = true,
                        Direccion = new Direccion()
                        {
                            Calle = "345 Spear St",
                            Estado = "CA",
                            Ciudad = "San Francisco",
                            Pais = "United States",
                        }

                    };
                    await userManager.CreateAsync(newAdminUser, "Admin00*");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                var adminUser2 = await userManager.FindByEmailAsync("santiago@tasq.com");
                if (adminUser2 == null)
                {
                    var newAdminUser2 = new AppUser()
                    {
                        Nombre = "Santiago",
                        UserName = "santiago@tasq.com",
                        Email = "santiago@tasq.com",
                        EmailConfirmed = true,
                        Direccion = new Direccion()
                        {
                            Calle = "Av Paseo de las Palmas",
                            Estado = "Mexico City",
                            Ciudad = "Mexico City",
                            Pais = "Mexico",
                        }

                    };
                    await userManager.CreateAsync(newAdminUser2, "Admin00*");
                    await userManager.AddToRoleAsync(newAdminUser2, UserRoles.Admin);
                }



                var adminUser3 = await userManager.FindByEmailAsync("admin@tasq.com");
                if (adminUser3 == null)
                {
                    var newAdminUser3 = new AppUser()
                    {
                        Nombre = "Admin",
                        UserName = "admin@tasq.com",
                        Email = "admin@tasq.com",
                        EmailConfirmed = true,
                        Direccion = new Direccion()
                        {
                            Calle = "Emerson 147",
                            Estado = "Mexico City",
                            Ciudad = "Mexico City",
                            Pais = "Mexico",
                        }
                    };
                    await userManager.CreateAsync(newAdminUser3, "Admin00*");
                    await userManager.AddToRoleAsync(newAdminUser3, UserRoles.Admin);
                }



            }
        }
    }

}

