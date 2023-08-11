using Core.Application.Constants;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.SeedData
{
	public static class DbInitializer
	{
		public static async Task SeedRoleAsync(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
		{
			await context.Database.EnsureCreatedAsync();

			string[] roles = { RoleConstants.Admin, RoleConstants.Staff, RoleConstants.Student };

			try
			{

				foreach (var role in roles)
				{
					if (!roleManager.RoleExistsAsync(role).Result)
					{
						var role1 = new IdentityRole
						{
							Name = role
						};
						roleManager.CreateAsync(role1).Wait();
					}
				}
				await context.SaveChangesAsync();

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}

		public static async Task SeedUserAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			try
			{
				var user = new ApplicationUser
				{
					FirstName = "Praise",
					LastName = "Balogun",
					Email = "globaladmin@gmail.com",
					UserName = "globaladmin",
					PhoneNumber = "08034879878",
					IsStudent = false
				};
				var foundUser = await userManager.FindByEmailAsync(user.Email);
				if (foundUser == null)
				{
					await userManager.CreateAsync(user, "Pa$$w0rd");
					await userManager.AddToRoleAsync(user, RoleConstants.Admin);
				}

				var adminUser = new ApplicationUser
				{
					FirstName = "Fortune",
					LastName = "Balogun",
					Email = "staff@gmail.com",
					UserName = "staff",
					PhoneNumber = "08034879228",
					IsStudent = false
				};

				var secondFoundUser = await userManager.FindByEmailAsync(adminUser.Email);
				if (secondFoundUser == null)
				{
					await userManager.CreateAsync(adminUser, "Pa$$w0rd");
					userManager.AddToRoleAsync(adminUser, RoleConstants.Staff).Wait();
				}


				var customer = new ApplicationUser
				{
					FirstName = "Onimsi",
					LastName = "Balogun",
					Email = "student@gmail.com",
					UserName = "customer",
					PhoneNumber = "08034879228",
					IsStudent = true
				};

				var customerUser = await userManager.FindByEmailAsync(adminUser.Email);
				if (customerUser == null)
				{
					await userManager.CreateAsync(adminUser, "Pa$$w0rd");
					userManager.AddToRoleAsync(adminUser, RoleConstants.Student).Wait();

				}
				await context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
	}
}
