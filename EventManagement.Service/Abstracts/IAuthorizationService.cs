namespace EventManagement.Service.Abstracts
{
	public interface IAuthorizationService
	{
		public Task<string> AddRoleAsync(string roleName);
		public Task<string> EditRoleAsync(int Id,string Name);
		public Task<bool> IsRoleExistAsync(string roleName);
		public Task<bool> IsRoleExistByIdAsync(int Id);
	}
}
