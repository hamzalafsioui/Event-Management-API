using EventManagement.Data.Entities;

namespace EventManagement.Service.Abstracts
{
	public interface ICommentService
	{
		public IQueryable<Comment> GetCommentsByEventIdQueryable(int eventId);
		public Task<List<Comment>> GetCommentsListByEventId(int eventId);
		public Task<string> AddAsync(Comment comment);
		public Task<string> UpdateAsync(Comment comment);
		Task<Comment> getCommentByIdAsync(int commentId);
		Task<List<Comment>> GetUserCommentsListByUserIdAsync(int userId);
		Task<bool> IsCommentExistByIdAsync(int commentId);
	}
}
