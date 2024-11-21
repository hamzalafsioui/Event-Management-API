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
		IRequestHandler<AddEventCommand, Response<string>>,
		IRequestHandler<EditEventCommand, Response<string>>,
		IRequestHandler<DeleteEventCommand, Response<string>>,
		IRequestHandler<CancelEventCommand, Response<string>>
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
			var result = await _eventService.AddAsync(newEvent, request.SpeakerIds);
			if (result.IsSuccess)
				return Created<string>(_stringLocalizer[SharedResourcesKeys.Created]);
			else
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAdd]);

		}

		public async Task<Response<string>> Handle(EditEventCommand request, CancellationToken cancellationToken)
		{
			// check is eventId exists
			var @event = await _eventService.GetEventByIdAsync(request.Id);
			if (@event == null)
				return NotFound<string>($"{_stringLocalizer[SharedResourcesKeys.EventId]} {request.Id} {_stringLocalizer[SharedResourcesKeys.NotFound]}");

			// mapping between event & request
			_mapper.Map(request, @event);
			// call update service
			var result = await _eventService.EditAsync(@event);
			if (result != null)
				return Success($"Edit Successfully In Id {@event.EventId}");
			else
				return BadRequest<string>();

		}

		public async Task<Response<string>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
		{
			// check is event exist
			var @event = await _eventService.GetEventByIdAsync(request.EventId);
			// return BadRequest if not exist
			if (@event == null)
				return NotFound<string>($"{_stringLocalizer[SharedResourcesKeys.EventId]} {request.EventId} {_stringLocalizer[SharedResourcesKeys.NotFound]}");
			// call delete service
			var result = await _eventService.DeleteAsync(@event);
			if (result)
				return Deleted<string>($"{_stringLocalizer[SharedResourcesKeys.EventId]} {@event.EventId} {_stringLocalizer[SharedResourcesKeys.Deleted]}");
			else
				return BadRequest<string>();
		}

		public async Task<Response<string>> Handle(CancelEventCommand request, CancellationToken cancellationToken)
		{

			// call cancel service
			var result = await _eventService.CancelAsync(request.EventId);
			if (result)
				return BadRequest<string>();
			return Success<string>($"{_stringLocalizer[SharedResourcesKeys.Updated]}");
		}
		#endregion



	}
}
