using EventManagement.Data.Entities;
using EventManagement.Data.Helper;
using EventManagement.Data.Helper.Enums;
using EventManagement.Infrustructure.Abstracts;
using EventManagement.Infrustructure.Repositories;
using EventManagement.Service.Abstracts;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq.Expressions;

namespace EventManagement.Service.Implementations
{
	internal class EventService : IEventService
	{
		#region Fields
		private readonly IEventRepository _eventRepository;
		private readonly ISpeakerRepository _speakerRepository;

		#endregion
		#region Constructors
		public EventService(IEventRepository eventRepository, ISpeakerRepository speakerRepository)
		{
			_eventRepository = eventRepository;
			_speakerRepository = speakerRepository;
		}

		#endregion
		#region Handle Functions
		public async Task<Event?> GetEventByIdAsync(int id)
		{
			var result = await _eventRepository.GetTableNoTracking().Where(x => x.EventId.Equals(id))
															  .Include(e => e.Creator)
															  .Include(e => e.Category)
															   .Include(e => e.Attendees).ThenInclude(a => a.User)
															   .Include(e => e.Comments).ThenInclude(c => c.User)
															   .Include(e => e.SpeakerEvents).ThenInclude(s => s.Speaker.User)
															  .FirstOrDefaultAsync();
			return result!;
		}


		public async Task<Result> AddAsync(Event @event, List<int>? speakerIds)
		{
			using (var transaction = await _eventRepository.BeginTransactionAsync())
			{
				try
				{
					// Add the event to the database
					var createdEvent = await _eventRepository.AddAsync(@event);
					if (createdEvent == null)
						return Result.Failure("Failed to create the event.");

					// Add speakers if provided
					if (speakerIds != null && speakerIds.Any())
					{
						foreach (var speakerId in speakerIds)
						{
							// Ensure the speaker exists options : we validate this in validator
							var speakerExists = await _speakerRepository.ExistsAsync(speakerId);
							if (!speakerExists)
								return Result.Failure($"Speaker with ID {speakerId} does not exist.");

							// Create SpeakerEvent relationship
							var speakerEvent = new SpeakerEvent
							{
								EventId = createdEvent.EventId,
								SpeakerId = speakerId
							};

							await _eventRepository.AddSpeakerToEventAsync(speakerEvent);
						}
					}

					await transaction.CommitAsync();
					return Result.Success();
				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					Log.Error($"Error adding event with speakers: {ex.Message}");
					return Result.Failure("An unexpected error occurred.");
				}

			}
		}
		public async Task<Result> EditAsync(Event @event, List<int>? speakerIds)
		{
			using var transaction = await _eventRepository.BeginTransactionAsync();

			try
			{
				// Update the event details
				var updatedEvent = await _eventRepository.UpdateAsync(@event);
				if (updatedEvent == null)
					return Result.Failure("Failed to update the event.");

				// Update speakers if provided
				if (speakerIds != null)
				{
					// Get the current speaker associations for the event
					var currentSpeakers = await _eventRepository.GetEventSpeakersAsync(@event.EventId);

					// Determine speakers to add and remove
					var speakersToAdd = speakerIds.Except(currentSpeakers.Select(s => s.SpeakerId)).ToList();
					var speakersToRemove = currentSpeakers.Where(s => !speakerIds.Contains(s.SpeakerId)).ToList();

					// Remove speakers no longer associated
					await _eventRepository.RemoveSpeakerRangeFromEventAsync(speakersToRemove);


					// Add new speakers
					foreach (var speakerId in speakersToAdd)
					{
						// Ensure the speaker exists
						var speakerExists = await _speakerRepository.ExistsAsync(speakerId);
						if (!speakerExists)
							return Result.Failure($"Speaker with ID {speakerId} does not exist.");

						// Add speaker to the event
						var speakerEvent = new SpeakerEvent
						{
							EventId = @event.EventId,
							SpeakerId = speakerId
						};
						await _eventRepository.AddSpeakerToEventAsync(speakerEvent);
					}
				}

				await transaction.CommitAsync();
				return Result.Success();
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				Log.Error($"Error in EditAsync: {ex.Message}");
				return Result.Failure("An unexpected error occurred while updating the event.");
			}
		}
		public async Task<Result> DeleteAsync(Event @event)
		{
			try
			{
				using var transaction = await _eventRepository.BeginTransactionAsync();
				// Perform delete
				var deleteEventResult = await _eventRepository.DeleteAsync(@event);

				if (!deleteEventResult)
					return Result.Failure("Failed to delete the event.");

				// Delete Speakers related with event { will be deleted automatically coz relation is onCascade }

				// Commit transaction
				await transaction.CommitAsync();

				return Result.Success();
			}
			catch (Exception ex)
			{
				Log.Error($"Error in DeleteAsync: {ex.Message}");
				return Result.Failure("An error occurred while deleting the event.");
			}
		}

		public async Task<List<Event>> GetEventsListAsync() => await _eventRepository.GetEventsListAsync();

		public IQueryable<Event> FilterEventsPaginatedQueryable(EventOrderingEnum orderingEnum, string search)
		{
			var queryable = _eventRepository.GetTableNoTracking();

			if (!string.IsNullOrEmpty(search))
			{
				queryable = queryable.Where(x => x.Title.Contains(search) ||
												 x.Creator.UserName!.Contains(search) ||
												 x.Location.Contains(search));
			}
			// include Speakers
			queryable = queryable.Include(x => x.SpeakerEvents).ThenInclude(x => x.Speaker.User);

			// Map the ordering enum to a sorting expression
			Expression<Func<Event, object>> orderExpression = orderingEnum switch
			{
				EventOrderingEnum.EventId => x => x.EventId,
				EventOrderingEnum.Title => x => x.Title,
				EventOrderingEnum.Location => x => x.Location,
				EventOrderingEnum.StartTime => x => x.StartTime,
				EventOrderingEnum.EndTime => x => x.EndTime,
				EventOrderingEnum.CategoryName => x => x.Category.Name,
				EventOrderingEnum.Creator => x => x.Creator.UserName!,
				EventOrderingEnum.CreatedAt => x => x.CreatedAt,
				_ => x => x.EventId
			};

			return queryable.OrderBy(orderExpression);
		}



		public async Task<bool> CancelAsync(int eventId)
		{
			var @event = await _eventRepository.GetTableAsTracking().FirstOrDefaultAsync(x => x.EventId == eventId);
			if (@event != null)
			{
				@event.Status = EventStatus.Canceled;
				return await _eventRepository.UpdateAsync(@event) != @event;

			}
			return false;
		}

		public async Task<List<Attendee>> GetEventAttendeesListByIdAsync(int eventId)
		{
			var eventWithAttendees = await _eventRepository.GetTableNoTracking()
					.Include(x => x.Attendees.Where(x => x.HasAttended))
					.ThenInclude(x => x.User)
					.SingleOrDefaultAsync(x => x.EventId == eventId);

			return eventWithAttendees?.Attendees.ToList() ?? new List<Attendee>();

		}

		public async Task<List<Event>> GetEventsListByCategoryId(int categoryId)
		{
			var result = await _eventRepository.GetTableNoTracking()
				.Where(x => x.CategoryId.Equals(categoryId))
				.Include(x => x.Creator)
				.Include(x => x.SpeakerEvents).ThenInclude(x => x.Speaker.User)
				.ToListAsync();
			return result;
		}

		public async Task<List<Event>> GetUpcomingOrPastEventsList(DateTimeComparison comparison)
		{
			var query = _eventRepository.GetTableNoTracking();

			// Apply filter based on the comparison type
			if (comparison == DateTimeComparison.Upcoming)
			{
				query = query.Where(x => x.StartTime > DateTime.UtcNow);
			}
			else
			{
				query = query.Where(x => x.StartTime <= DateTime.UtcNow);
			}

			return await query.Include(x => x.Creator)
				.Include(x => x.SpeakerEvents)
				.ThenInclude(x => x.Speaker.User)
				.ToListAsync();
		}


		#endregion


	}
}
