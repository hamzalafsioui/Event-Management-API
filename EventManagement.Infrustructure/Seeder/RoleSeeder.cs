using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Helper.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrustructure.Seeder
{
	public static class RoleSeeder
	{
		public static async Task SeedAsync(RoleManager<Role> _roleManager)
		{
			var rolesCount = await _roleManager.Roles.CountAsync();
			if (rolesCount <= 0)
			{
				foreach(var role in Enum.GetValues(typeof(UserRoleEnum))) { 
					await _roleManager.CreateAsync(new Role()
					{
						Name = role.ToString(),
					});
				}
				
			}
		}
	}
}
