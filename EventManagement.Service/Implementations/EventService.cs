﻿using EventManagement.Data.Entities;
using EventManagement.Data.Helper.Enums;
using EventManagement.Infrustructure.Repositories;
using EventManagement.Service.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Service.Implementations
{
	internal class EventService : IEventService
	{
		#region Fields
		private readonly IEventRepository _eventRepository;

		#endregion
		#region Constructors
		public EventService(IEventRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		#endregion
		#region Handle Functions
		public async Task<Event> GetEventByIdAsync(int id)
		{
			var result = await _eventRepository.GetTableNoTracking().Where(x => x.EventId.Equals(id))
															  .Include(e => e.Creator)
															  .Include(e => e.Category)
															   .Include(e => e.Attendees).ThenInclude(a => a.User)
															   .Include(e => e.Comments).ThenInclude(c => c.User)
															  .FirstOrDefaultAsync();
			return result!;
		}


		public async Task<bool> AddAsync(Event @event)
		{
			// we can checking DB is already Exist this Event
			await _eventRepository.AddAsync(@event);
			return true;

		}

		public async Task<List<Event>> GetEventsListAsync()
		{
			return await _eventRepository.GetEventsListAsync();
		}

		public IQueryable<Event> FilterEventsPaginatedQueryable(EventOrderingEnum orderingEnum, string search)
		{
			var queryable = _eventRepository.GetTableNoTracking().AsQueryable();

			if (!string.IsNullOrEmpty(search))
			{
				queryable = queryable.Where(x => x.Title.Contains(search) || x.Creator.UserName.Contains(search) || x.Location.Contains(search));

			}
			switch (orderingEnum)
			{
				case EventOrderingEnum.EventId:
					queryable = queryable.OrderBy(x => x.EventId);
					break;
				case EventOrderingEnum.Title:
					queryable = queryable.OrderBy(x => x.Title);
					break;
				case EventOrderingEnum.Location:
					queryable = queryable.OrderBy(x => x.Location);
					break;
				case EventOrderingEnum.StartTime:
					queryable = queryable.OrderBy(x => x.StartTime);
					break;
				case EventOrderingEnum.EndTime:
					queryable = queryable.OrderBy(x => x.EndTime);
					break;
				case EventOrderingEnum.CategoryName:
					queryable = queryable.OrderBy(x => x.Category.Name);
					break;
				case EventOrderingEnum.Creator:
					queryable = queryable.OrderBy(x => x.Creator.UserName);
					break;
				case EventOrderingEnum.CreatedAt:
					queryable = queryable.OrderBy(x => x.CreatedAt);
					break;
				default:
					queryable = queryable.OrderBy(x => x.EventId);
					break;
			}
			return queryable;
		}

		public async Task<bool> EditAsync(Event @event)
		{
			try
			{
				await _eventRepository.UpdateAsync(@event);
				await _eventRepository.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> DeleteAsync(Event @event)
		{
			try
			{
				await _eventRepository.DeleteAsync(@event);
				await _eventRepository.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> CancelAsync(int eventId)
		{
			var @event = await _eventRepository.GetTableAsTracking().FirstOrDefaultAsync(x => x.EventId == eventId);
			if (@event != null)
			{
				@event.Status = EventStatus.Canceled;
				await _eventRepository.UpdateAsync(@event);
				return true;
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
