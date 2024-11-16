using EventManagement.Data.Entities;

namespace EventManagement.Service.Abstracts
{
	public interface IAttendeeService
	{
		IQueryable<Attendee> GetAttendeesByEventIdQueryable(int eventId);
		Task<Attendee> AddAsync(Attendee attendee);
		Task<Attendee?> GetAttendeeByUserIdEventIdAsync(int userId, int eventId);
		Task<Attendee> UpdateAsyc(Attendee attendee);
		Task<bool> DeleteAsync(Attendee attendee);
		Task<List<Attendee>> GetEventsByUserIdAsync(int userId);
		public Task<bool> IsUserAttendedEvent(int eventId, int userId);

	}
}
