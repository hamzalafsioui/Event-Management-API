using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Entities.SPs;
using EventManagement.Data.Entities.Views;
using EventManagement.Data.Helper.Enums;

namespace EventManagement.Service.Abstracts
{
	public interface IUserService
	{
		public Task<List<User>> GetUsersListAsync();
		public IQueryable<User> GetUsersListQueryable();
		public IQueryable<User> FilterUserPaginatedQueryable(UserOrderingEnum orderingEnum, string search);
		public Task<User> GetByIdWithIncludeAsync(int id);
		public Task<User?> GetByIdAsync(int id);

		public Task<string> AddAsync(User user, string password);
		public Task<bool> IsUserNameExist(string name);
		public Task<bool> IsUserNameExistExcludeSelf(string username, int id);
		Task<User> EditAsync(User userMapper);
		Task<bool> CustomDeleteAsync(User user);
		public Task<List<ViewUserEventEngagementSummary>> GetViewUserEventEngagementSummaryAsync();

		public Task<SP_GetUserEventEngagementDetails> GetUserEventEngagementDetailsAsync(SP_GetUserEventEngagementDetailsParameters parameters);
	}
}
