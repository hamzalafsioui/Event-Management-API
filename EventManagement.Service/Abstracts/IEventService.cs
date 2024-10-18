using EventManagement.Data.Entities;

namespace EventManagement.Service.Abstracts
{
	public interface IEventService
	{
		public Task<Event> GetEventByIdAsync(int id);
		public Task<string> AddAsync(Event @event);
	}
}
