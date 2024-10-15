using EventManagement.Data.Entities;
using EventManagement.Infrustructure.InfrustructureBase;

namespace EventManagement.Infrustructure.Repositories
{
	public interface IEventRepository : IGenericRepositoryAsync<Event>
	{
		public Task<List<Event>> GetEventsListAsync();

	}

}