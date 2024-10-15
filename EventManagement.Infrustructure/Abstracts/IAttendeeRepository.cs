using EventManagement.Data.Entities;
using EventManagement.Infrustructure.InfrustructureBase;

namespace EventManagement.Infrustructure.Repositories
{
	public interface IAttendeeRepository : IGenericRepositoryAsync<Attendee>
	{
		public Task<List<Attendee>> GetAttendeesListAsync();

	}

}