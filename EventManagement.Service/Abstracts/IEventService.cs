using EventManagement.Data.Entities;
using EventManagement.Data.Helper.Enums;

namespace EventManagement.Service.Abstracts
{
	public interface IEventService
	{
		public Task<Event> GetEventByIdAsync(int id);
		public Task<string> AddAsync(Event @event);
		public Task<List<Event>> GetEventsListAsync();
		public IQueryable<Event> FilterEventsPaginatedQueryable(EventOrderingEnum orderingEnum, string search);
		public Task<string> EditAsync(Event @event);
		public Task<string> DeleteAsync(Event @event);
		public Task<string> CancelAsync(int eventId);

	}
}
