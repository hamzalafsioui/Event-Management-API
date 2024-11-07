using EventManagement.Data.Entities.Identity;
using EventManagement.Service.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace EventManagement.Service.Implementations
{
	public class AuthorizationService : IAuthorizationService
	{
		#region Fields
		private readonly RoleManager<Role> _roleManager;
		private readonly UserManager<User> _userManager;

		#endregion
		#region Constructors
		public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
		}
		#endregion
		#region Handle Functions

		public async Task<string> AddRoleAsync(string roleName)
		{
			var identityRole = new Role();
			identityRole.Name = roleName;
			var result = await _roleManager.CreateAsync(identityRole);
			if (result.Succeeded)
				return "Success";
			return "Failed";

		}

		public async Task<string> DeleteRoleAsync(int Id)
		{
			var role = await _roleManager.FindByIdAsync(Id.ToString());
			if (role == null) return "NotFound";
			// is role exist in any user
			var users = await _userManager.GetUsersInRoleAsync(role.Name!);
			// return exception
			if (users.Any()) return "Used";
			//delete
			var result = await _roleManager.DeleteAsync(role);
			//success
			return result.Succeeded ? "Success" : string.Join("|-|", result.Errors.Select(x => x.Description).ToString() ?? "Failed");

		}

		public async Task<string> EditRoleAsync(int Id, string roleName)
		{
			// check role is exist 
			var role = await _roleManager.FindByIdAsync(Id.ToString());
			// return not found
			if (role == null)
			{
				return "NotFound";
			}
			// edit 
			role.Name = roleName;
			var result = await _roleManager.UpdateAsync(role);
			// operation success
			return result.Succeeded ? "Success" : string.Join("|-|", result.Errors.Select(x => x.Description).ToString() ?? "Failed");

		}

		public async Task<bool> IsRoleExistAsync(string roleName)
		{
			return await _roleManager.RoleExistsAsync(roleName);
		}

		public async Task<bool> IsRoleExistByIdAsync(int Id)
		{
			var role = await _roleManager.FindByIdAsync(Id.ToString());
			return role != null;
		}

		#endregion
	}
}
