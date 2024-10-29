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
		#endregion

		#region Constructors
		public EventRepository(AppDbContext dbContext) : base(dbContext)
		{
			_events = dbContext.Set<Event>();
		}


		#endregion

		#region Handl Functions
		public async Task<List<Event>> GetEventsListAsync()
		{
			var @events = await _events.AsNoTracking()
				.Include(x => x.Category)
				.Include(x => x.Creator)
				.ToListAsync();

			return events;
		}

		public override IQueryable<Event> GetTableNoTracking()
		{
			return base.GetTableNoTracking().Include(e => e.Category).Include(e => e.Creator);
		}

		#endregion

	}
}


