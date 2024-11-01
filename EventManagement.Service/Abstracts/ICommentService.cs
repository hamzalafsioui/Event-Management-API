using EventManagement.Data.Entities;

namespace EventManagement.Service.Abstracts
{
	public interface ICommentService
	{
		public IQueryable<Comment> GetCommentsByEventIdQueryable(int eventId);
		public Task<List<Comment>> GetCommentsListByEventId(int eventId);
		public Task<string> AddAsync(Comment comment);
		Task<Comment> getCommentByIdAsync(int commentId);
	}
}
