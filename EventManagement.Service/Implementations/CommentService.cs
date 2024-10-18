using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Repositories;
using EventManagement.Service.Abstracts;

namespace EventManagement.Service.Implementations
{
	public class CommentService : ICommentService
	{
		private readonly ICommentRepository _commentRepository;
		#region Fields

		#endregion
		#region Constructors
		public CommentService(ICommentRepository commentRepository)
		{
			_commentRepository = commentRepository;
		}
		#endregion
		#region Actions
		public IQueryable<Comment> GetCommentsByEventIdQueryable(int eventId)
		{
			return _commentRepository.GetTableNoTracking().Where(e => e.EventId.Equals(eventId)).AsQueryable();
		}
		#endregion

	}
}
