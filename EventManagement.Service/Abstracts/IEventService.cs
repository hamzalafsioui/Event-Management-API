using EventManagement.Data.Entities;
using EventManagement.Data.Helper;

namespace EventManagement.Service.Abstracts
{
	public interface IEventService
	{
		public Task<Event> GetEventByIdAsync(int id);
		public Task<string> AddAsync(Event @event);
		public Task<List<Event>> GetEventsListAsync();
		public IQueryable<Event> FilterEventsPaginatedQueryable(EventOrderingEnum orderingEnum, string search);
		public Task<string> EditAsync(Event @event);
	}
}
