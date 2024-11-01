using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Context;
using EventManagement.Infrustructure.InfrustructureBase;
using EventManagement.Infrustructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrustructure.Abstracts
{
	public class CommentRepository : GenericRepositoryAsync<Comment>, ICommentRepository
	{
		#region Fields
		private readonly DbSet<Comment> _comments;
		#endregion

		#region Constructors
		public CommentRepository(AppDbContext dbContext) : base(dbContext)
		{
			_comments = dbContext.Set<Comment>();
		}


		#endregion

		#region Handl Functions
		public async Task<List<Comment>> GetCommentsListAsync()
		{
			return await _comments.ToListAsync();
		}

		#endregion

	}
}


