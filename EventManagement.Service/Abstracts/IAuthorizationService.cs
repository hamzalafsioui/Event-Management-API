using EventManagement.Data.Entities.Identity;

namespace EventManagement.Service.Abstracts
{
	public interface IAuthorizationService
	{
		public Task<string> AddRoleAsync(string roleName);
		public Task<string> EditRoleAsync(int Id,string Name);
		public Task<string> DeleteRoleAsync(int Id);
		public Task<bool> IsRoleExistAsync(string roleName);
		public Task<bool> IsRoleExistByIdAsync(int Id);
		public Task<List<Role>> GetRolesListAsync();
	}
}
