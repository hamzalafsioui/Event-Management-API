namespace EventManagement.Service.Abstracts
{
	public interface IAuthorizationService
	{
		public Task<string> AddRoleAsync(string roleName);
		public Task<bool> IsRoleExistAsync(string roleName);
	}
}
