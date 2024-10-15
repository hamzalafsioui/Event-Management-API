using EventManagement.Data.Entities;
using EventManagement.Infrustructure.InfrustructureBase;

namespace EventManagement.Infrustructure.Repositories
{
	public interface ICommentRepository : IGenericRepositoryAsync<Comment>
	{
		public Task<List<Comment>> GetCommentsListAsync();

	}

}