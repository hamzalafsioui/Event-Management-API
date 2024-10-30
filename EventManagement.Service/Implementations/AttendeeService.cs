using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Repositories;
using EventManagement.Service.Abstracts;
using Microsoft.EntityFrameworkCore;

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

		public async Task<string> UpdateAsyc(Attendee attendee)
		{
			await _attendeeRepository.UpdateAsync(attendee);
			return "Success";
		}

		public async Task<Attendee> GetAttendeeByUserIdEventIdAsync(int userId, int eventId)
		{
			var attendee = await _attendeeRepository.GetTableNoTracking()
				.Where(a => a.UserId.Equals(userId) && a.EventId.Equals(eventId))
				.FirstOrDefaultAsync();
			return attendee!;
		}

		public async Task<string> DeleteAsync(Attendee attendee)
		{
			await _attendeeRepository.DeleteAsync(attendee);
			return "Success";
		}
		#endregion

	}
}
