using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Events.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Events.Commands.Handlers
{
	public class EventCommandHandler : ResponseHandler,
		IRequestHandler<AddEventCommand, Response<string>>
	{
		private readonly IEventService _eventService;
		private readonly IMapper _mapper;
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;

		#endregion
		#region Constructors
		public EventCommandHandler(IEventService eventService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_eventService = eventService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
		}
		#endregion
		#region Handle Functions
		public async Task<Response<string>> Handle(AddEventCommand request, CancellationToken cancellationToken)
		{
			// mapping 
			var newEvent = _mapper.Map<Event>(request);
			// call add event service
			var result = await _eventService.AddAsync(newEvent);
			if (result == "Success")
				return Success("Added Successfully");
			else
				return BadRequest<string>();

		}
		#endregion



	}
}
