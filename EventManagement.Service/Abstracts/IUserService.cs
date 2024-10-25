using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Helper.Enums;

namespace EventManagement.Service.Abstracts
{
    public interface IUserService
	{
		public Task<List<User>> GetUsersListAsync();
		public IQueryable<User> GetUsersListQueryable();
		public IQueryable<User> FilterUserPaginatedQueryable(UserOrderingEnum orderingEnum, string search);
		public Task<User> GetByIdWithIncludeAsync(int id);
		public Task<User> GetByIdAsync(int id);

		public Task<string> AddAsync(User user);
		public Task<bool> IsUserNameExist(string name);
		public Task<bool> IsUserNameExistExcludeSelf(string username, int id);
		Task<string> EditAsync(User userMapper);
		Task<string> DeleteAsync(User user);
	}
}
