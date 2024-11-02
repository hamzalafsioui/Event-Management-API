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

		public async Task<bool> AddAsync(Comment comment) => await _commentRepository.AddAsync(comment);

		public async Task<bool> UpdateAsync(Comment comment) => await _commentRepository.UpdateAsync(comment);

		public async Task<bool> DeleteAsync(Comment comment) => await _commentRepository.DeleteAsync(comment);

		public async Task<Comment> GetCommentByIdAsync(int commentId)
		{
			var result = await _commentRepository.GetTableNoTracking()
				.Where(x => x.CommentId.Equals(commentId))
				.Include(x => x.User)
				.FirstOrDefaultAsync();

			return result!;
		}
		#endregion
		#region Actions
		public IQueryable<Comment> GetCommentsByEventIdQueryable(int eventId)
		{
			return _commentRepository.GetTableNoTracking().Where(e => e.EventId.Equals(eventId)).AsQueryable();
		}

		public async Task<int> GetCommentsCountForEvent(int eventId)
		{
			return await _commentRepository.GetTableNoTracking()
				.Where(x => x.EventId.Equals(eventId))
				.CountAsync();
		}

		public async Task<List<Comment>> GetCommentsListByEventId(int eventId)
		{
			return await _commentRepository.GetTableNoTracking()
				.Where(x => x.EventId.Equals(eventId))
				.Include(x => x.User)
				.ToListAsync();
		}

		public async Task<List<Comment>> GetUserCommentsListByUserIdAsync(int userId)
		{
			return await _commentRepository.GetTableNoTracking()
				.Where(x => x.UserId.Equals(userId))
				.Include(x => x.Event)
				.ToListAsync();
		}

		public async Task<bool> IsCommentExistByIdAsync(int commentId)
		{
			return await _commentRepository.GetTableNoTracking()
				.AnyAsync(x => x.CommentId.Equals(commentId));
			
		}

		#endregion

	}
}
