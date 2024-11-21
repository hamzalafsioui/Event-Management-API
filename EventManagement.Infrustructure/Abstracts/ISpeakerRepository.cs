using EventManagement.Data.Entities;
using EventManagement.Infrustructure.InfrustructureBase;

namespace EventManagement.Infrustructure.Abstracts
{
	public interface ISpeakerRepository : IGenericRepositoryAsync<Speaker>
	{
		/// <summary>
		/// Retrieves a list of all speakers asynchronously.
		/// </summary>
		/// <returns>A list of <see cref="Speaker"/> entities.</returns>
		public Task<List<Speaker>> GetSpeakersListAsync();

		public Task<bool> ExistsAsync(int speakerId);
	}
}
