using EventManagement.Data.Entities;

namespace EventManagement.Service.Abstracts
{
	public interface ICommentService
	{
		public IQueryable<Comment> GetCommentsByEventIdQueryable(int eventId);
		public Task<List<Comment>> GetCommentsListByEventId(int eventId);
		public Task<bool> AddAsync(Comment comment);
		public Task<bool> UpdateAsync(Comment comment);
		Task<Comment> GetCommentByIdAsync(int commentId);
		Task<List<Comment>> GetUserCommentsListByUserIdAsync(int userId);
		Task<bool> IsCommentExistByIdAsync(int commentId);
		Task<int> GetCommentsCountForEvent(int eventId);
		Task<bool> DeleteAsync(Comment comment);
	}
}
