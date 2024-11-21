using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Context;
using EventManagement.Infrustructure.InfrustructureBase;
using EventManagement.Infrustructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrustructure.Abstracts
{
	public class EventRepository : GenericRepositoryAsync<Event>, IEventRepository
	{
		#region Fields
		private readonly DbSet<Event> _events;
		private readonly DbSet<SpeakerEvent> _speakerEvent;
		#endregion

		#region Constructors
		public EventRepository(AppDbContext dbContext) : base(dbContext)
		{
			_events = dbContext.Set<Event>();
			_speakerEvent = dbContext.Set<SpeakerEvent>();
		}


		#endregion

		#region Handl Functions

		// Get all events with their related entities (Category and Creator)
		public async Task<List<Event>> GetEventsListAsync()
		{
			var @events = await _events.AsNoTracking()
				.Include(x => x.Category)
				.Include(x => x.Creator)
				.ToListAsync();

			return events;
		}

		// Override GetTableNoTracking to include related entities by default
		public override IQueryable<Event> GetTableNoTracking()
		{
			return base.GetTableNoTracking().Include(e => e.Category).Include(e => e.Creator);
		}

		// paginated Event
		public async Task<List<Event>> GetEventsPagedAsync(int pageIndex, int pageSize)
		{
			return await _events.Skip(pageIndex * pageSize)
				.Take(pageSize)
				.Include(e => e.Category)
				.Include(e => e.Creator)
				.ToListAsync();
		}
		// Get events by creator id
		public async Task<List<Event>> GetEventsByCreatorIdAsync(int creatorId)
		{
			return await _events.Where(e => e.CreatorId == creatorId)
				.AsNoTracking()
				.Include(e => e.Category)
				.Include(e => e.Creator)
				.ToListAsync();
		}
		// Get events by category id
		public async Task<List<Event>> GetEventsByCategoryIdAsync(int categoryId)
		{
			return await _events.Where(e => e.CategoryId == categoryId)
				.Include(e => e.Category)
				.Include(e => e.Creator)
				.ToListAsync();
		}

		public async Task<bool> AddSpeakerToEventAsync(SpeakerEvent speakerEvent)
		{
			return await _speakerEvent.AddAsync(speakerEvent) != null;
		}
		#endregion

	}
}


