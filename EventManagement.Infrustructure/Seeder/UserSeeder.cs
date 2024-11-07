﻿using EventManagement.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrustructure.Seeder
{
	public static class UserSeeder
	{
		public static async Task SeedAsync(UserManager<User> _userManager)
		{
			var usersCount = await _userManager.Users.CountAsync();
			if(usersCount <= 0)
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
				await _userManager.CreateAsync(defaultUser, "Mr@Lafsioui");
				await _userManager.AddToRoleAsync(defaultUser, "Admin");
			}
		}
	}
}