using EventManagement.Data.Entities;
using EventManagement.Infrustructure.InfrustructureBase;

namespace EventManagement.Infrustructure
{
	public interface ISpeakerEventRepository : IGenericRepositoryAsync<SpeakerEvent>
	{
		public Task<List<SpeakerEvent>> GetSpeakerEventListAsync();
	}
}
