using EventManagement.Data.Entities;

namespace EventManagement.Service.Abstracts
{
	public interface IAttendeeService
	{
		IQueryable<Attendee> GetAttendeesByEventIdQueryable(int eventId);
		Task<string> AddAsync(Attendee attendee);
		Task<Attendee> GetAttendeeByUserIdEventIdAsync(int userId, int eventId);
		Task<string> UpdateAsyc(Attendee attendee);
		Task<string> DeleteAsync(Attendee attendee);
	}
}
