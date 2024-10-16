using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Events.Queries.Models;
using EventManagement.Core.Features.Events.Queries.Response;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Events.Queries.Handlers
{
	public class EventQueryHandler : ResponseHandler,
		IRequestHandler<GetEventByIdQuery, Response<GetEventByIdResponse>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IEventService _eventService;
		private readonly IMapper _mapper;
		#endregion

		#region Consructors
		public EventQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IEventService eventService, IMapper mapper) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_eventService = eventService;
			_mapper = mapper;
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
			// response 
			return Success(eventMapper);

		}
		#endregion

	}
}
