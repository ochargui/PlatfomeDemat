using DEMAT.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace DEMAT.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            string[] roleNames = { "Super-utilisateur", "Agent-numerisation", "Controleur-un-type", "Controleur-multi-type", "Agent-typage" };
            IdentityResult result;
            foreach (var roleName in roleNames)
            {
                // creating the roles and seeding them to the database
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    result = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            // creating users with roles and seeding them to the database
            var admin = new AppUser()
            {

                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                DisplayName = "Admin",
                FirstName = "System",
                LastName = "Admin",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                EmailConfirmed = true
            };
            if (!userManager.Users.Any(x => x.Email == admin.Email))
            {
                await userManager.CreateAsync(admin, "Password@1");
                await userManager.AddToRoleAsync(admin, "Super-utilisateur");
            }

            var agent = new AppUser()
            {
                Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                Email = "agent@localhost.com",
                NormalizedEmail = "AGENT@LOCALHOST.COM",
                DisplayName = "Agent",
                FirstName = "System",
                LastName = "Agent",
                UserName = "agent@localhost.com",
                NormalizedUserName = "AGENT@LOCALHOST.COM",
                EmailConfirmed = true
            };
            if (!userManager.Users.Any(x => x.Email == agent.Email))
            {
                await userManager.CreateAsync(agent, "Password@1");
                await userManager.AddToRoleAsync(agent, "Agent-numerisation");
            }

            var agentControle = new AppUser()
            {
                Id = "7e224968-33e4-4652-b7b7-8574d048cdb9",
                Email = "controleur@localhost.com",
                NormalizedEmail = "CONTROLEUR@LOCALHOST.COM",
                DisplayName = "Controleur",
                FirstName = "System",
                LastName = "controleur",
                UserName = "controleur@localhost.com",
                NormalizedUserName = "CONTROLEUR@LOCALHOST.COM",
                EmailConfirmed = true
            };
            if (!userManager.Users.Any(x => x.Email == agentControle.Email))
            {
                await userManager.CreateAsync(agentControle, "Password@1");
                await userManager.AddToRoleAsync(agentControle, "Controleur-un-type");
            }

            var testUser = new AppUser()
            {
                Id = "1e224979-33e8-4652-b7b7-8574d048cdb9",
                Email = "firas.belhiba@esprit.tn",
                NormalizedEmail = "FIRAS.BELHIBA@ESPRIT.TN",
                DisplayName = "Controleur",
                FirstName = "System",
                LastName = "controleur",
                UserName = "firas.belhiba@esprit.tn",
                NormalizedUserName = "FIRAS.BELHIBA@ESPRIT.TN",
                EmailConfirmed = true
            };
            if (!userManager.Users.Any(x => x.Email == testUser.Email))
            {
                await userManager.CreateAsync(testUser, "Password@1");
                await userManager.AddToRoleAsync(testUser, "Controleur-un-type");
            }

        }
    }
}
