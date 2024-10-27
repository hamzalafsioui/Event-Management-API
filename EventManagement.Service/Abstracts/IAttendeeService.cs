using EventManagement.Data.Entities;

namespace EventManagement.Service.Abstracts
{
	public interface IAttendeeService
	{
		IQueryable<Attendee> GetAttendeesByEventIdQueryable(int eventId);
		Task<string> AddAsync(Attendee attendee);
	}
}
