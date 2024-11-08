using EventManagement.Data.DTOs.Roles;
using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Requests;
using EventManagement.Data.Responses;

namespace EventManagement.Service.Abstracts
{
	public interface IAuthorizationService
	{
		#region Roles
		public Task<string> AddRoleAsync(string roleName);
		public Task<string> EditRoleAsync(int Id, string Name);
		public Task<string> DeleteRoleAsync(int Id);
		public Task<bool> IsRoleExistAsync(string roleName);
		public Task<bool> IsRoleExistByIdAsync(int Id);
		public Task<List<Role>> GetRolesListAsync();
		public Task<Role> GetRoleByIdAsync(int id);
		public Task<ManageUserRolesResponse> ManageUserRolesDate(User user);
		public Task<string> UpdateUserRoles(ManageUserRolesRequest request);

		#endregion

		#region Claims
		public Task<ManageUserClaimsResponse> ManageUserClaimsData(User user);
		#endregion

	}
}
