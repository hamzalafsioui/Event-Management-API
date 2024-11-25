using EventManagement.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EventManagement.Infrustructure.Seeder
{
	public static class UserSeeder
	{
		public static async Task SeedAsync(UserManager<User> _userManager)
		{
			var usersCount = await _userManager.Users.CountAsync();
			IEnumerable<Claim> userClaims = new List<Claim>()
			{
				new Claim(type: "Create Event",true.ToString().ToLower()),
				new Claim(type: "Edit Event",true.ToString().ToLower()),
				new Claim(type: "Cancel Event",true.ToString().ToLower()),
				new Claim(type: "Delete Event",true.ToString().ToLower()),
			};
			if (usersCount <= 0)
			{
				var defaultUser = new User
				{
					UserName = "hamzalafsioui",
					FirstName = "hamza",
					LastName = "lafsioui",
					Email = "hamzalafsioui@gmail.com",
					Image = null,
					EmailConfirmed = true,
					PhoneNumberConfirmed = true,
					DateOfBirth = new DateTime(2000, 03, 18),
					Role = Data.Helper.Enums.UserRoleEnum.Admin,

				};
				await _userManager.CreateAsync(defaultUser, "Mr@Lafsioui2024");
				await _userManager.AddToRoleAsync(defaultUser, "Admin");
				await _userManager.AddClaimsAsync(defaultUser, userClaims);
			}
		}
	}
}
