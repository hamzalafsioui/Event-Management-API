using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Context;
using EventManagement.Infrustructure.InfrustructureBase;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrustructure.Repositories
{
	public interface IUserRepository : IGenericRepositoryAsync<User>
	{
		public Task<List<User>> GetUsersListAsync();

	}
}