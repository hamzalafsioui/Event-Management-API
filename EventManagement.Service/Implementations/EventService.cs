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
		public async Task<Event> EditAsync(Event @event) => await _eventRepository.UpdateAsync(@event);
		public async Task<bool> DeleteAsync(Event @event) => await _eventRepository.DeleteAsync(@event);


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

			return await query.Include(x => x.Creator).ToListAsync();
		}
		#endregion


	}
}
