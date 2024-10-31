using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Events.Queries.Models;
using EventManagement.Core.Features.Events.Queries.Response;
using EventManagement.Core.Resources;
using EventManagement.Core.Wrappers;
using EventManagement.Data.Entities;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;

namespace EventManagement.Core.Features.Events.Queries.Handlers
{
	public class EventQueryHandler : ResponseHandler,
		IRequestHandler<GetEventByIdQuery, Response<GetEventByIdResponse>>,
		IRequestHandler<GetEventListQuery, Response<List<GetEventListResponse>>>,
		IRequestHandler<GetEventPaginatedListQuery, PaginatedResult<GetEventPaginatedListResponse>>,
		IRequestHandler<GetEventAttendeesQuery, Response<List<GetEventAttendeesResponse>>>,
		IRequestHandler<GetEventsListByCategoryIdQuery, Response<List<GetEventsListByCategoryIdResponse>>>,
		IRequestHandler<GetUpcomingOrPastEventsListQuery, Response<List<GetUpcomingOrPastEventsListResponse>>>

	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IEventService _eventService;
		private readonly IMapper _mapper;
		private readonly IAttendeeService _attendeeService;
		private readonly ICommentService _commentService;
		#endregion

		#region Consructors
		public EventQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IEventService eventService, IMapper mapper, IAttendeeService attendeeService, ICommentService commentService) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_eventService = eventService;
			_mapper = mapper;
			_attendeeService = attendeeService;
			_commentService = commentService;
		}


		#endregion

		#region Handle Functions
		public async Task<Response<GetEventByIdResponse>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
		{
			// Invalid Id
			if (request.Id < 1)
				return BadRequest<GetEventByIdResponse>(_stringLocalizer[SharedResourcesKeys.InvalidId]);
			var response = await _eventService.GetEventByIdAsync(request.Id);
			// check is Exist
			if (response == null)
				return NotFound<GetEventByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

			// mapping 
			var eventMapper = _mapper.Map<GetEventByIdResponse>(response);

			// Pagination
			Expression<Func<Attendee, AttendeeResponse>> expressionAttend = eA => new AttendeeResponse(eA.User.UserName!);
			Expression<Func<Comment, CommentResponse>> expressionComment = eC => new CommentResponse(eC.CommentId, eC.User.UserName!, eC.Content);
			var AttendeesQueryable = _attendeeService.GetAttendeesByEventIdQueryable(request.Id);
			var CommentsQueryable = _commentService.GetCommentsByEventIdQueryable(request.Id);

			var AttendePaginatedList = await AttendeesQueryable.Select(expressionAttend).ToPaginatedListAsync(request.AttendeePageNumber, request.AttendeePageSize);
			var CommentPaginatedList = await CommentsQueryable.Select(expressionComment).ToPaginatedListAsync(request.CommentPageNumber, request.CommentPageSize);
			eventMapper.AttendeesList = AttendePaginatedList;
			eventMapper.CommentsList = CommentPaginatedList;

			// response 
			return Success(eventMapper);

		}

		public async Task<Response<List<GetEventListResponse>>> Handle(GetEventListQuery request, CancellationToken cancellationToken)
		{
			// cal event service to retrieve list
			var eventList = await _eventService.GetEventsListAsync();
			// initial mapping
			var eventListMapping = _mapper.Map<List<GetEventListResponse>>(eventList);
			// transfer data to response handler
			var result = Success(eventListMapping);
			result.Meta = new
			{
				count = eventListMapping.Count,
			};
			// return result
			return result;
		}

		public async Task<PaginatedResult<GetEventPaginatedListResponse>> Handle(GetEventPaginatedListQuery request, CancellationToken cancellationToken)
		{
			Expression<Func<Event, GetEventPaginatedListResponse>> expression = e => new GetEventPaginatedListResponse(
													e.EventId, e.Title, e.Description, e.Location, e.StartTime, e.EndTime,
													e.Category.Name, e.Creator.UserName!, e.Capacity, e.CreatedAt);

			var filterQuery = _eventService.FilterEventsPaginatedQueryable(request.OrderBy, request.Search!);
			var paginatedList = await filterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
			paginatedList.Meta = new
			{
				Count = paginatedList.Data.Count,
			};
			return paginatedList;

		}

		public async Task<Response<List<GetEventAttendeesResponse>>> Handle(GetEventAttendeesQuery request, CancellationToken cancellationToken)
		{
			// check is event exist
			var @event = await _eventService.GetEventByIdAsync(request.EventId);
			if (@event == null)
				return BadRequest<List<GetEventAttendeesResponse>>();

			// call event service
			var EventAttendeesList = await _eventService.GetEventAttendeesListByIdAsync(request.EventId);

			// mapping 
			var EventAttendeesListMapping = _mapper.Map<List<GetEventAttendeesResponse>>(EventAttendeesList);
			// return response
			return Success(EventAttendeesListMapping);
		}

		public async Task<Response<List<GetEventsListByCategoryIdResponse>>> Handle(GetEventsListByCategoryIdQuery request, CancellationToken cancellationToken)
		{
			// call event service
			var eventList = await _eventService.GetEventsListByCategoryId(request.categoryId);
			// mapping 
			var eventListMapping = _mapper.Map<List<GetEventsListByCategoryIdResponse>>(eventList);
			// return result
			return Success(eventListMapping);
		}

		public async Task<Response<List<GetUpcomingOrPastEventsListResponse>>> Handle(GetUpcomingOrPastEventsListQuery request, CancellationToken cancellationToken)
		{
			// call event service 
			var eventList = await _eventService.GetUpcomingOrPastEventsList(request.Comparison);
			// mapping 
			var eventListMapping = _mapper.Map<List<GetUpcomingOrPastEventsListResponse>>(eventList);
			// return result
			return Success(eventListMapping);
		}
		#endregion

	}
}
