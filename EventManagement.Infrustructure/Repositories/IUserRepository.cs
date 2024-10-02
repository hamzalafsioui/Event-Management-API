using EventManagement.Data.Entities;
using EventManagement.Infrustructure.InfrustructureBase;

namespace EventManagement.Infrustructure.Repositories
{
	public interface IUserRepository:IGenericRepositoryAsync<User>
	{
		public Task<List<User>> GetUsersListAsync();
	}
}
