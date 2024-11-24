using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Abstracts;
using EventManagement.Infrustructure.Context;
using EventManagement.Infrustructure.InfrustructureBase;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrustructure.Repositories
{
	public class SpeakerEventRepository : GenericRepositoryAsync<SpeakerEvent>, ISpeakerEventRepository
	{
		#region Fields
		private readonly DbSet<SpeakerEvent> _speakerEvent;
		#endregion
		#region Constructors
		public SpeakerEventRepository(AppDbContext dbContext) : base(dbContext)
		{
			_speakerEvent = dbContext.Set<SpeakerEvent>();
		}


		#endregion

		#region Actions
		public async Task<List<SpeakerEvent>> GetSpeakerEventListAsync()
		{
			return await _speakerEvent.ToListAsync();
		}
		#endregion
	}
}
