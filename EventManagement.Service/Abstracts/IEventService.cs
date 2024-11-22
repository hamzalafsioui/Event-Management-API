using EventManagement.Data.Entities;
using EventManagement.Data.Helper;
using EventManagement.Data.Helper.Enums;

namespace EventManagement.Service.Abstracts
{
	/// <summary>
	/// Interface defining operations related to events management.
	/// </summary>
	public interface IEventService
	{
		/// <summary>
		/// Retrieves an event by its <paramref name="id"/> asynchronously.
		/// </summary>
		/// <param name="id">The unique identifier of the event.</param>
		/// <returns>The <see cref="Event"/> entity if found; otherwise, <see langword="null"/>.</returns>
		public Task<Event?> GetEventByIdAsync(int id);

		/// <summary>
		/// Adds a new <paramref name="@event"/> to the system asynchronously.
		/// </summary>
		/// <param name="@event">The <see cref="Event"/> entity to add.</param>
		/// <returns><see cref="Result"/> class with values <see langword="IsSuccess"/> and <see langword="ErrorMessage"/>.</returns>
		public Task<Result> AddAsync(Event @event, List<int>? speakerIds);

		/// <summary>
		/// Retrieves a list of all events asynchronously.
		/// </summary>
		/// <returns>A list of <see cref="Event"/> entities.</returns>
		public Task<List<Event>> GetEventsListAsync();

		/// <summary>
		/// Filters and paginates events based on the specified <paramref name="orderingEnum"/> and <paramref name="search"/> criteria.
		/// </summary>
		/// <param name="orderingEnum">The ordering criteria for events.</param>
		/// <param name="search">The search string to filter events.</param>
		/// <returns>An <see cref="IQueryable{T}"/> of filtered <see cref="Event"/> entities.</returns>
		public IQueryable<Event> FilterEventsPaginatedQueryable(EventOrderingEnum orderingEnum, string search);

		/// <summary>
		/// Updates the specified <paramref name="event"/> asynchronously.
		/// </summary>
		/// <param name="event">The updated <see cref="Event"/> entity.</param>
		/// <returns><see cref="Result"/> class with values <see langword="IsSuccess"/> and <see langword="ErrorMessage"/>.</returns>
		public Task<Result> EditAsync(Event @event, List<int> speakerIds);

		/// <summary>
		/// Deletes the specified <paramref name="event"/> from the system asynchronously.
		/// </summary>
		/// <param name="event">The <see cref="Event"/> entity to delete.</param>
		/// <returns><see cref="Result"/> class with values <see langword="IsSuccess"/> and <see langword="ErrorMessage"/>.</returns>
		public Task<Result> DeleteAsync(Event @event);

		/// <summary>
		/// Cancels the event with the specified <paramref name="eventId"/> asynchronously.
		/// </summary>
		/// <param name="eventId">The unique identifier of the event to cancel.</param>
		/// <returns><see langword="true"/> if the operation succeeded; otherwise, <see langword="false"/>.</returns>
		public Task<bool> CancelAsync(int eventId);

		/// <summary>
		/// Retrieves a list of attendees for the specified <paramref name="eventId"/> asynchronously.
		/// </summary>
		/// <param name="eventId">The unique identifier of the event.</param>
		/// <returns>A list of <see cref="Attendee"/> entities associated with the event.</returns>
		public Task<List<Attendee>> GetEventAttendeesListByIdAsync(int eventId);

		/// <summary>
		/// Retrieves a list of events associated with the specified <paramref name="categoryId"/> asynchronously.
		/// </summary>
		/// <param name="categoryId">The unique identifier of the category.</param>
		/// <returns>A list of <see cref="Event"/> entities in the specified category.</returns>
		public Task<List<Event>> GetEventsListByCategoryId(int categoryId);

		/// <summary>
		/// Retrieves a list of either upcoming or past events based on the specified <paramref name="comparison"/> criteria asynchronously.
		/// </summary>
		/// <param name="comparison">The <see cref="DateTimeComparison"/> criterion for filtering events.</param>
		/// <returns>A list of <see cref="Event"/> entities that match the comparison criterion.</returns>
		public Task<List<Event>> GetUpcomingOrPastEventsList(DateTimeComparison comparison);

	}
}
