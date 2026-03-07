using GymManagementDAL.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.DataSeed
{
    public class IdentityContextSeeding
    {
       public static async Task<bool> SeedData(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            try
            {
                var hasUsers = userManager.Users.Any();
                var hasRoles = roleManager.Roles.Any();
                if (hasRoles && hasUsers) return false;
                if (!hasRoles)
                {
                    var Roles = new List<IdentityRole>()
                {
                    new(){Name="SuperAdmin"},
                    new(){Name="Admin"}

                };
                    foreach (var Role in Roles)
                    {
                        await roleManager.CreateAsync(Role);
                    }


                }
                if (!hasUsers)
                {
                    var SuperAdmin = new ApplicationUser()
                    {
                        FirstName = "Ahmed",
                        LastName = "Tawfik",
                        UserName = "AhmedMohamed",
                        Email = "ahmedmohamed2042003@gmail.com",
                        PhoneNumber = "01021811865",

                    };
                    await userManager.CreateAsync(SuperAdmin, "P@ssw0rd");
                    await userManager.AddToRoleAsync(SuperAdmin, "SuperAdmin");
                    var Admin = new ApplicationUser()
                    {
                        FirstName = "MohamedSakran",
                        LastName = "Sakran",
                        UserName = "MohamedSakran",
                        Email = "MohamedSakran@gmail.com",
                        PhoneNumber = "01015151515",

                    };

                    await userManager.CreateAsync(Admin, "P@ssw0rd");
                    await userManager.AddToRoleAsync(Admin, "Admin");




                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Seed Failed {ex}");
                return false;
            }
        }

        }
    }

