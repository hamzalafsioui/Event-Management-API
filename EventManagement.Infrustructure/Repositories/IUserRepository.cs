using EventManagement.Data.Entities;

namespace EventManagement.Infrustructure.Repositories
{
	public interface IUserRepository
	{
		public Task<List<User>> GetUsersListAsync();
	}
}
