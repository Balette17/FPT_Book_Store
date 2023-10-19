using FPTBookStore.Models;
using Microsoft.AspNetCore.Identity;

namespace FPTBookStore.Data
{
	public static class ContextSeed
	{
		public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			//Seed Roles
			await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Owner.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.User.ToString()));
        }
		public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			//Seed Default User
			var defaultUser = new ApplicationUser
			{
				UserName = "trannguyetcantsga@gmail.com",
				Email = "trannguyetcantsga@gmail.com",
				FirstName = "Tran",
				LastName = "Nguyet Can",
				EmailConfirmed = true,
				PhoneNumberConfirmed = true
			};
			if (userManager.Users.All(u => u.Id != defaultUser.Id))
			{
				var user = await userManager.FindByEmailAsync(defaultUser.Email);
				if (user == null)
				{
					await userManager.CreateAsync(defaultUser, "Can@127300");;
					await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());
                    await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Owner.ToString()));
                    await roleManager.CreateAsync(new IdentityRole(Enums.Roles.User.ToString()));
                }

			}
		}
	}
}