using EventManagement.Data.Entities.Identity;

namespace EventManagement.Service.AuthService.Interfaces
{
	public interface ICurrentUserService
	{
		public int GetUserId();
		public Task<User> GetCurrentUserAsync();
		public Task<List<string>> GetCurrentUserRolesAsync();
	}
}
