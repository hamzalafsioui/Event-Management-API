using EventManagement.Data.Entities;

namespace EventManagement.Service.Abstracts
{
	public interface IUserService
	{
		public Task<List<User>> GetUsersListAsync();
		public Task<User> GetUserByIdAsync(int id);

		public Task<string> AddAsync(User user);
		public Task<bool> IsUserNameExist(string name);
		public Task<bool> IsUserNameNameExistExcludeSelf(string username, int id);
	}
}
