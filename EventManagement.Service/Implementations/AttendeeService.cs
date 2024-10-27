using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Repositories;
using EventManagement.Service.Abstracts;

namespace EventManagement.Service.Implementations
{
	public class AttendeeService : IAttendeeService
	{
		#region Fields
		private readonly IAttendeeRepository _attendeeRepository;
		#endregion
		#region Constructors
		public AttendeeService(IAttendeeRepository attendeeRepository)
		{
			_attendeeRepository = attendeeRepository;
		}


		#endregion

		#region Actions
		public IQueryable<Attendee> GetAttendeesByEventIdQueryable(int eventId)
		{
			return _attendeeRepository.GetTableNoTracking().Where(x => x.EventId.Equals(eventId)).AsQueryable();
		}
		public async Task<string> AddAsync(Attendee attendee)
		{
			await _attendeeRepository.AddAsync(attendee);
			return "Success";
		}
		#endregion

	}
}
