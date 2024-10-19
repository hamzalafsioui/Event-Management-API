using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Repositories;
using EventManagement.Service.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Service.Implementations
{
	internal class EventService : IEventService
	{
		#region Fields
		private readonly IEventRepository _eventRepository;

		#endregion
		#region Constructors
		public EventService(IEventRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		#endregion
		#region Handle Functions
		public async Task<Event> GetEventByIdAsync(int id)
		{

			var result = await _eventRepository.GetTableNoTracking().Where(x => x.EventId.Equals(id))
															  .Include(e => e.Creator)
															  .Include(e => e.Category)
															  // .Include(e => e.Attendees).ThenInclude(a => a.User)
															  // .Include(e => e.Comments).ThenInclude(c => c.User)
															  .FirstOrDefaultAsync();
			return result;
		}


		public async Task<string> AddAsync(Event @event)
		{
			// we can checking DB is already Exist this Event
			await _eventRepository.AddAsync(@event);
			return "Success";

		}

		public async Task<List<Event>> GetEventsListAsync()
		{
			return await _eventRepository.GetEventsListAsync();
		}
		#endregion


	}
}
