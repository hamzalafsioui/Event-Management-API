using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Context;
using EventManagement.Infrustructure.InfrustructureBase;
using EventManagement.Infrustructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrustructure.Abstracts
{
	public class AttendeeRepository : GenericRepositoryAsync<Attendee>, IAttendeeRepository
	{
		#region Fields
		private readonly DbSet<Attendee> _attendees;
		#endregion

		#region Constructors
		public AttendeeRepository(AppDbContext dbContext) : base(dbContext)
		{
			_attendees = dbContext.Set<Attendee>();
		}


		#endregion

		#region Handl Functions
		public async Task<List<Attendee>> GetAttendeesListAsync()
		{
			return await _attendees.ToListAsync();
		}


		#endregion

	}
}


