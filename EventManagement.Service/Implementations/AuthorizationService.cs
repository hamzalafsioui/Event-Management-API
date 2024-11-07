using EventManagement.Data.DTOs.Roles;
using EventManagement.Data.Entities.Identity;
using EventManagement.Infrustructure.Context;
using EventManagement.Service.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Service.Implementations
{
	public class AuthorizationService : IAuthorizationService
	{
		#region Fields
		private readonly RoleManager<Role> _roleManager;
		private readonly UserManager<User> _userManager;
		private readonly AppDbContext _dbContext;

		#endregion
		#region Constructors
		public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager, AppDbContext dbContext)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_dbContext = dbContext;
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
				return "NotFound";

			// edit 
			role.Name = roleName;
			var result = await _roleManager.UpdateAsync(role);
			// operation success
			return result.Succeeded ? "Success" : string.Join("|-|", result.Errors.Select(x => x.Description).ToString() ?? "Failed");

		}

		public async Task<Role> GetRoleByIdAsync(int id)
		{
			var role = await _roleManager.FindByIdAsync(id.ToString());
			return role;
		}

		public async Task<List<Role>> GetRolesListAsync()
		{
			var roles = await _roleManager.Roles.ToListAsync();
			return roles;
		}

		public async Task<ManageUserRolesResponse> GetUserRolesListAsync(User user)
		{
			var response = new ManageUserRolesResponse();
			var rolesList = new List<UserRoles>();
			// user roles
			var userRoles = await _userManager.GetRolesAsync(user);
			var Roles = new List<UserRoles>();
			// roles
			var roles = await _roleManager.Roles.ToListAsync();
			response.UserId = user.Id;
			foreach (var role in roles)
			{
				var userRole = new UserRoles()
				{
					Id = role.Id,
					Name = role.Name!,
					HasRole = userRoles.Contains(role.Name!) ? true : false
				};
				rolesList.Add(userRole);
			}

			response.Roles = rolesList;
			return response;

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

		public async Task<string> UpdateUserRoles(ManageUserRolesRequest request)
		{
			using (var transaction = await _dbContext.Database.BeginTransactionAsync())
			{
				try
				{
					// get user with old roles
					var user = await _userManager.FindByIdAsync(request.UserId.ToString());
					if (user == null)
						return "UserNotFound";
					// get old roles
					var userRoles = await _userManager.GetRolesAsync(user);
					// delete Old Roles
					var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
					// Add Roles
					if (!removeResult.Succeeded)
						return "FailedToRemoveOldRoles";
					var selectedRoles = request.Roles.Where(x => x.HasRole == true).Select(x => x.Name);
					var addRoleResult = await _userManager.AddToRolesAsync(user, selectedRoles);
					// return result
					if (!addRoleResult.Succeeded)
						return "FailedToAddNewRoles";
					await transaction.CommitAsync();
					return "Success";

				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					return "FailedToUpdateUserRoles";
				}

			}

		}

		#endregion
	}
}
