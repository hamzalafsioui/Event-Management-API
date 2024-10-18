using EventManagement.Data.Entities;

namespace EventManagement.Service.Abstracts
{
	public interface ICommentService
	{
		public IQueryable<Comment> GetCommentsByEventIdQueryable(int eventId);
	}
}
