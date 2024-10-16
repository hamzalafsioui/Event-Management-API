using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Abstracts;
using EventManagement.Service.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Service.Implementations
{
	public class EventService : IEventService
	{
		#region Fields
		private readonly EventRepository _eventRepository;

		#endregion
		#region Constructors
		public EventService(EventRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}
		#endregion

		#region Handle Functions
		public async Task<Event> GetEventByIdAsync(int id)
		{
			var result = await _eventRepository.GetTableNoTracking().Where(e => e.EventId.Equals(id))
												.Include(e => e.Creator)
												.Include(e=>e.Attendees).ThenInclude(a=>a.User)
												.Include(e=>e.Comments).ThenInclude(c=>c.User)
												.FirstOrDefaultAsync();
			return result!;
		}
		#endregion

	}
}
