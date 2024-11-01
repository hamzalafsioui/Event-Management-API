using EventManagement.Data.Entities;
using EventManagement.Data.Helper.Enums;

namespace EventManagement.Service.Abstracts
{
	public interface IEventService
	{
		public Task<Event> GetEventByIdAsync(int id);
		public Task<bool> AddAsync(Event @event);
		public Task<List<Event>> GetEventsListAsync();
		public IQueryable<Event> FilterEventsPaginatedQueryable(EventOrderingEnum orderingEnum, string search);
		public Task<bool> EditAsync(Event @event);
		public Task<bool> DeleteAsync(Event @event);
		public Task<bool> CancelAsync(int eventId);
		public Task<List<Attendee>> GetEventAttendeesListByIdAsync(int eventId);
		public Task<List<Event>> GetEventsListByCategoryId(int categoryId);
		public Task<List<Event>> GetUpcomingOrPastEventsList(DateTimeComparison comparison);

	}
}
