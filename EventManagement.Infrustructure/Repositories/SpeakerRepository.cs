using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Abstracts;
using EventManagement.Infrustructure.Context;
using EventManagement.Infrustructure.InfrustructureBase;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrustructure.Repositories
{
	public class SpeakerRepository : GenericRepositoryAsync<Speaker>, ISpeakerRepository
	{
		#region Fields
		private readonly DbSet<Speaker> _speakers;
		#endregion

		#region Constructors
		public SpeakerRepository(AppDbContext dbContext) : base(dbContext)
		{
			_speakers = dbContext.Set<Speaker>();
		}

		public async Task<bool> ExistsAsync(int speakerId)
		{
			return (await _speakers.FirstOrDefaultAsync(x=>x.Id == speakerId)) != null;
		}


		#endregion

		#region Handl Functions

		public async Task<List<Speaker>> GetSpeakersListAsync()
		{
			return await _speakers.Include(x => x.User).ToListAsync();
		}
		#endregion
	}
}
