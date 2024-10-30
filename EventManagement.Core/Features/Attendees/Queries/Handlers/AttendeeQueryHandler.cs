using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Attendees.Queries.Models;
using EventManagement.Core.Features.Attendees.Queries.Responses;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Attendees.Queries.Handlers
{
	public class AttendeeQueryHandler : ResponseHandler,
		IRequestHandler<GetRegisteredEventsForUserQuery, Response<List<GetRegisteredEventsListForUserResponse>>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IAttendeeService _attendeeService;
		private readonly IMapper _mapper;

		#endregion

		#region Constructors
		public AttendeeQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
			IAttendeeService attendeeService,
			IMapper mapper) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			this._attendeeService = attendeeService;
			this._mapper = mapper;
		}

		#endregion
		#region Handle functions
		public async Task<Response<List<GetRegisteredEventsListForUserResponse>>> Handle(GetRegisteredEventsForUserQuery request, CancellationToken cancellationToken)
		{
			// retrieve data by calling attendee service
			var RegisteredEvents = await _attendeeService.GetEventsByUserIdAsync(request.userId);
			// mapping
			var RegisteredEventsMapping = _mapper.Map<List<GetRegisteredEventsListForUserResponse>>(RegisteredEvents);

			return Success(RegisteredEventsMapping);
		}

		#endregion



	}
}
