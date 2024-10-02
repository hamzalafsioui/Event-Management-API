using EventManagement.Data.Entities;

namespace EventManagement.Service.Abstracts
{
	public interface IUserService
	{
		public Task<List<User>> GetUsersListAsync();
		public Task<User> GetUserByIdAsync(int id);
	}
}
