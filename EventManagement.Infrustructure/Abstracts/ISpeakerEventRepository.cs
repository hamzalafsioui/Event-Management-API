using EventManagement.Data.Entities;
using EventManagement.Infrustructure.InfrustructureBase;

namespace EventManagement.Infrustructure.Abstracts
{
	public interface ISpeakerEventRepository : IGenericRepositoryAsync<SpeakerEvent>
	{
		public Task<List<SpeakerEvent>> GetSpeakerEventListAsync();
	}
}
