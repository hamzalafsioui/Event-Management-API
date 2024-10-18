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
		IRequestHandler<GetEventByIdQuery, Response<GetEventByIdResponse>>
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
			Expression<Func<Attendee, AttendeeResponse>> expressionAttend = eA => new AttendeeResponse(eA.AttendeeId, eA.User.Username);
			Expression<Func<Comment, CommentResponse>> expressionComment = eC => new CommentResponse(eC.CommentId, eC.User.Username, eC.Content);
			var AttendeesQueryable = _attendeeService.GetAttendeesByEventIdQueryable(request.Id);
			var CommentsQueryable = _commentService.GetCommentsByEventIdQueryable(request.Id);

			var AttendePaginatedList = await AttendeesQueryable.Select(expressionAttend).ToPaginatedListAsync(request.AttendeePageNumber, request.AttendeePageSize);
			var CommentPaginatedList = await CommentsQueryable.Select(expressionComment).ToPaginatedListAsync(request.CommentPageNumber, request.CommentPageSize);
			eventMapper.AttendeesList = AttendePaginatedList;
			eventMapper.CommentsList = CommentPaginatedList;

			// response 
			return Success(eventMapper);

		}
		#endregion

	}
}
