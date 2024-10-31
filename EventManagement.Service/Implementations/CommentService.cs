using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Repositories;
using EventManagement.Service.Abstracts;
using Microsoft.EntityFrameworkCore;

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

		public async Task<string> AddAsync(Comment comment)
		{
			await _commentRepository.AddAsync(comment);
			return "Success";
		}
		#endregion
		#region Actions
		public IQueryable<Comment> GetCommentsByEventIdQueryable(int eventId)
		{
			return _commentRepository.GetTableNoTracking().Where(e => e.EventId.Equals(eventId)).AsQueryable();
		}
		public async Task<List<Comment>> GetCommentsListByEventId(int eventId)
		{
			return await _commentRepository.GetTableNoTracking()
				.Where(x => x.EventId.Equals(eventId))
				.Include(x => x.User)
				.ToListAsync();
		}

		#endregion

	}
}
