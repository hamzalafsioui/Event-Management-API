using EventManagement.Data.Entities;

namespace EventManagement.Service.Abstracts
{
	/// <summary>
	/// Interface defining operations related to event comments.
	/// </summary>
	public interface ICommentService
	{
		/// <summary>
		/// Retrieves a queryable collection of comments for a specific event by its <paramref name="eventId"/>.
		/// </summary>
		/// <param name="eventId">The unique identifier of the event.</param>
		/// <returns>An <see cref="IQueryable{T}"/> of <see cref="Comment"/> entities.</returns>
		public IQueryable<Comment> GetCommentsByEventIdQueryable(int eventId);

		/// <summary>
		/// Retrieves a list of comments for a specific event by its <paramref name="eventId"/> asynchronously.
		/// </summary>
		/// <param name="eventId">The unique identifier of the event.</param>
		/// <returns>A list of <see cref="Comment"/> entities.</returns>
		public Task<List<Comment>> GetCommentsListByEventId(int eventId);

		/// <summary>
		/// Adds a new <paramref name="comment"/> to the database asynchronously.
		/// </summary>
		/// <param name="comment">The <see cref="Comment"/> entity to add.</param>
		/// <returns>The added <see cref="Comment"/> entity.</returns>
		public Task<Comment> AddAsync(Comment comment);

		/// <summary>
		/// Updates an existing <paramref name="comment"/> in the database asynchronously.
		/// </summary>
		/// <param name="comment">The <see cref="Comment"/> entity to update.</param>
		/// <returns>The updated <see cref="Comment"/> entity.</returns>
		public Task<Comment> UpdateAsync(Comment comment);

		/// <summary>
		/// Retrieves a comment by its <paramref name="commentId"/> asynchronously.
		/// </summary>
		/// <param name="commentId">The unique identifier of the comment.</param>
		/// <returns>The <see cref="Comment"/> entity if found; otherwise, null.</returns>
		Task<Comment?> GetCommentByIdAsync(int commentId);

		/// <summary>
		/// Retrieves a list of comments made by a specific user identified by <paramref name="userId"/> asynchronously.
		/// </summary>
		/// <param name="userId">The unique identifier of the user.</param>
		/// <returns>A list of <see cref="Comment"/> entities.</returns>
		Task<List<Comment>> GetUserCommentsListByUserIdAsync(int userId);

		/// <summary>
		/// Checks if a comment exists in the database by its <paramref name="commentId"/> asynchronously.
		/// </summary>
		/// <param name="commentId">The unique identifier of the comment.</param>
		/// <returns><c>true</c> if the comment exists; otherwise, <c>false</c>.</returns>
		Task<bool> IsCommentExistByIdAsync(int commentId);

		/// <summary>
		/// Retrieves the total count of comments for a specific event identified by <paramref name="eventId"/> asynchronously.
		/// </summary>
		/// <param name="eventId">The unique identifier of the event.</param>
		/// <returns>The count of comments as an <see cref="int"/>.</returns>
		Task<int> GetCommentsCountForEvent(int eventId);

		/// <summary>
		/// Deletes a <paramref name="comment"/> from the database asynchronously.
		/// </summary>
		/// <param name="comment">The <see cref="Comment"/> entity to delete.</param>
		/// <returns><c>true</c> if the delete operation succeeded; otherwise, <c>false</c>.</returns>
		Task<bool> DeleteAsync(Comment comment);
	}
}
