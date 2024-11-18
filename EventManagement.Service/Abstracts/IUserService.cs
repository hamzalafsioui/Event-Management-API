using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Entities.SPs;
using EventManagement.Data.Entities.Views;
using EventManagement.Data.Helper.Enums;

namespace EventManagement.Service.Abstracts
{
	/// <summary>
	/// Interface defining user-related service operations.
	/// </summary>
	public interface IUserService
	{
		/// <summary>
		/// Retrieves a list of all <see cref="User"/> entities asynchronously.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{T}"/> of <see cref="User"/> entities.</returns>
		public Task<List<User>> GetUsersListAsync();

		/// <summary>
		/// Retrieves a queryable collection of <see cref="User"/> entities.
		/// </summary>
		/// <returns>An <see cref="IQueryable{T}"/> of <see cref="User"/> entities.</returns>
		public IQueryable<User> GetUsersListQueryable();

		/// <summary>
		/// Filters and paginates <see cref="User"/> entities based on the specified <paramref name="orderingEnum"/> and <paramref name="search"/> criteria.
		/// </summary>
		/// <param name="orderingEnum">The <see cref="UserOrderingEnum"/> to order the users by.</param>
		/// <param name="search">The search string to filter <see cref="User"/> entities.</param>
		/// <returns>An <see cref="IQueryable{T}"/> of filtered <see cref="User"/> entities.</returns>
		public IQueryable<User> FilterUserPaginatedQueryable(UserOrderingEnum orderingEnum, string search);

		/// <summary>
		/// Retrieves a <see cref="User"/> by its <paramref name="id"/> with related data included asynchronously.
		/// </summary>
		/// <param name="id">The unique identifier of the <see cref="User"/>.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="User"/> entity with related data.</returns>
		public Task<User> GetByIdWithIncludeAsync(int id);

		/// <summary>
		/// Retrieves a <see cref="User"/> by its <paramref name="id"/> asynchronously.
		/// </summary>
		/// <param name="id">The unique identifier of the <see cref="User"/>.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="User"/> entity, or <see langword="null"/> if not found.</returns>
		public Task<User?> GetByIdAsync(int id);

		/// <summary>
		/// Adds a new <see cref="User"/> with the specified <paramref name="password"/> asynchronously.
		/// </summary>
		/// <param name="user">The <see cref="User"/> entity to add.</param>
		/// <param name="password">The password for the <paramref name="user"/>.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="string"/> representing the result of the operation.</returns>
		public Task<string> AddAsync(User user, string password);

		/// <summary>
		/// Checks if a <paramref name="userName"/> exists in the system.
		/// </summary>
		/// <param name="userName">The <paramref name="userName"/> to check.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if the username exists; otherwise, <see langword="false"/>.</returns>
		public Task<bool> IsUserNameExist(string userName);

		/// <summary>
		/// Checks if a <paramref name="username"/> exists in the system, excluding the user with the specified <paramref name="id"/>.
		/// </summary>
		/// <param name="username">The <paramref name="username"/> to check.</param>
		/// <param name="id">The unique identifier of the <see cref="User"/> to exclude from the check.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if the username exists; otherwise, <see langword="false"/>.</returns>
		public Task<bool> IsUserNameExistExcludeSelf(string username, int id);


		/// <summary>
		/// Updates the specified <paramref name="user"/> asynchronously.
		/// </summary>
		/// <param name="user">The updated <see cref="User"/> entity.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the updated <see cref="User"/> entity.</returns>
		Task<User> EditAsync(User user);

		/// <summary>
		/// Performs a custom delete operation on the specified <paramref name="user"/>.
		/// </summary>
		/// <param name="user">The <see cref="User"/> entity to delete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if the delete operation succeeded; otherwise, <see langword="false"/>.</returns>
		Task<bool> CustomDeleteAsync(User user);

		/// <summary>
		/// Retrieves a summary of user event engagement asynchronously.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{T}"/> of <see cref="ViewUserEventEngagementSummary"/> entities.</returns>
		public Task<List<ViewUserEventEngagementSummary>> GetViewUserEventEngagementSummaryAsync();

		/// <summary>
		/// Retrieves detailed user event engagement information based on the specified <paramref name="parameters"/> asynchronously.
		/// </summary>
		/// <param name="parameters">The parameters for the stored procedure.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="SP_GetUserEventEngagementDetails"/> entity containing the engagement details.</returns>
		public Task<SP_GetUserEventEngagementDetails> GetUserEventEngagementDetailsAsync(SP_GetUserEventEngagementDetailsParameters parameters);
	}
}
