﻿using EventManagement.Data.Entities;
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
		public async Task<Attendee> AddAsync(Attendee attendee) =>	 await _attendeeRepository.AddAsync(attendee);
			
		public async Task<Attendee> UpdateAsyc(Attendee attendee) => await _attendeeRepository.UpdateAsync(attendee);
		public async Task<bool> DeleteAsync(Attendee attendee) => await _attendeeRepository.DeleteAsync(attendee);



		public async Task<Attendee?> GetAttendeeByUserIdEventIdAsync(int userId, int eventId)
		{
			var attendee = await _attendeeRepository.GetTableNoTracking()
				.Where(a => a.UserId.Equals(userId) && a.EventId.Equals(eventId))
				.FirstOrDefaultAsync();
			return attendee;
		}


		public async Task<List<Attendee>> GetEventsByUserIdAsync(int userId)
		{
			var registeredEvents = await _attendeeRepository.GetTableNoTracking()
				.Where(x => x.UserId.Equals(userId))
				.Include(x => x.Event)
				.ToListAsync();
			return registeredEvents;
		}

		public async Task<bool> IsUserAttendedEvent(int eventId, int userId)
		{
			var result = await _attendeeRepository.GetTableNoTracking()
													.AnyAsync(x => x.EventId.Equals(eventId) && x.UserId.Equals(userId) && x.HasAttended);
			return result;
		}
		#endregion

	}
}
