using EventManagement.Data.Entities;
using EventManagement.Infrustructure.InfrustructureBase;

namespace EventManagement.Infrustructure.Repositories
{
	public interface IEventRepository : IGenericRepositoryAsync<Event>
	{
		public Task<List<Event>> GetEventsListAsync();
		public Task<List<Event>> GetEventsPagedAsync(int pageIndex, int pageSize);
		public Task<List<Event>> GetEventsByCreatorIdAsync(int creatorId);
		public Task<List<Event>> GetEventsByCategoryIdAsync(int categoryId);

	}

}