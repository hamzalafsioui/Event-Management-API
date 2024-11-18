using EventManagement.Data.Entities;

namespace EventManagement.Service.Abstracts
{
	/// <summary>
	/// Provides methods for managing attendees in the event management system.
	/// </summary>
	public interface IAttendeeService
	{

		/// <summary>
		/// Gets a queryable collection of attendees for a specific event.
		/// </summary>
		/// <param name="eventId">The ID of the event.</param>
		/// <returns>
		/// A queryable collection of <see cref="Attendee"/> entities for the specified event.
		/// </returns>
		IQueryable<Attendee> GetAttendeesByEventIdQueryable(int eventId);

		/// <summary>
		/// Adds a new attendee to the event.
		/// </summary>
		/// <param name="attendee">The attendee entity to be added.</param>
		/// <returns>
		/// A task representing the asynchronous operation, with a result of the added <see cref="Attendee"/>.
		/// </returns>
		Task<Attendee> AddAsync(Attendee attendee);

		/// <summary>
		/// Retrieves an attendee by user ID and event ID.
		/// </summary>
		/// <param name="userId">The ID of the user.</param>
		/// <param name="eventId">The ID of the event.</param>
		/// <returns>
		/// A task representing the asynchronous operation, with a result of the <see cref="Attendee"/> if found, or null if not found.
		/// </returns>
		Task<Attendee?> GetAttendeeByUserIdEventIdAsync(int userId, int eventId);

		/// <summary>
		/// Updates an existing attendee's details.
		/// </summary>
		/// <param name="attendee">The attendee entity with updated information.</param>
		/// <returns>
		/// A task representing the asynchronous operation, with a result of the updated <see cref="Attendee"/>.
		/// </returns>
		Task<Attendee> UpdateAsyc(Attendee attendee);

		/// <summary>
		/// Deletes the specified attendee from the event.
		/// </summary>
		/// <param name="attendee">The attendee entity to be deleted.</param>
		/// <returns>
		/// A task representing the asynchronous operation, with a boolean indicating whether the deletion was successful.
		/// </returns>
		Task<bool> DeleteAsync(Attendee attendee);

		/// <summary>
		/// Retrieves a list of events the specified user has attended.
		/// </summary>
		/// <param name="userId">The ID of the user.</param>
		/// <returns>
		/// A task representing the asynchronous operation, with a list of <see cref="Attendee"/> entities the user has attended.
		/// </returns>
		Task<List<Attendee>> GetEventsByUserIdAsync(int userId);

		/// <summary>
		/// Checks if a user has attended a specific event.
		/// </summary>
		/// <param name="eventId">The ID of the event.</param>
		/// <param name="userId">The ID of the user.</param>
		/// <returns>
		/// A task representing the asynchronous operation, with a boolean indicating whether the user has attended the event.
		/// </returns>
		public Task<bool> IsUserAttendedEvent(int eventId, int userId);

	}
}
