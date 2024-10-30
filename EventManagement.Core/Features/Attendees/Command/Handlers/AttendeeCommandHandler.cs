using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Attendees.Command.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Attendees.Command.Handlers
{
	public class AttendeeCommandHandler : ResponseHandler,
		IRequestHandler<AddAttendeeCommand, Response<string>>,
		IRequestHandler<EditAttendeeCommand, Response<string>>,
		IRequestHandler<LeaveEventCommand, Response<string>>,
		IRequestHandler<ChangeRSVPStatusCommand, Response<string>>,
		IRequestHandler<MarkAttendanceCommand,Response<string>>
	{
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IAttendeeService _attendeeService;
		private readonly IMapper _mapper;
		#region Fields

		#endregion
		#region Consturctors
		public AttendeeCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IAttendeeService attendeeService,
			IMapper mapper) : base(stringLocalizer)
		{
			this._stringLocalizer = stringLocalizer;
			_attendeeService = attendeeService;
			this._mapper = mapper;
		}
		#endregion
		#region Handle Functions
		public async Task<Response<string>> Handle(AddAttendeeCommand request, CancellationToken cancellationToken)
		{
			// mapping 
			var attendeeMapping = _mapper.Map<Attendee>(request);
			// call add attendee service
			var result = await _attendeeService.AddAsync(attendeeMapping);
			if (result != "Success")
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAdd]);
			return Created<string>(_stringLocalizer[SharedResourcesKeys.Created]);
		}

		public async Task<Response<string>> Handle(EditAttendeeCommand request, CancellationToken cancellationToken)
		{
			var attendee = await _attendeeService.GetAttendeeByUserIdEventIdAsync(request.UserId, request.EventId);

			// mapping 
			var attendeeMapping = _mapper.Map<Attendee>(request);
			// handle RSVPDate
			if (attendeeMapping.Status != attendee.Status)
				attendeeMapping.RSVPDate = DateTime.UtcNow;
			else
				attendeeMapping.RSVPDate = attendee.RSVPDate;
			// call add attendee service
			var result = await _attendeeService.UpdateAsyc(attendeeMapping);
			if (result != "Success")
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);

			return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
		}

		public async Task<Response<string>> Handle(LeaveEventCommand request, CancellationToken cancellationToken)
		{
			// check is event exist
			var attendee = await _attendeeService.GetAttendeeByUserIdEventIdAsync(request.UserId, request.EventId);
			// return BadRequest if not exist
			if (attendee == null)
				return NotFound<string>($"{_stringLocalizer[SharedResourcesKeys.EventId]} {request.EventId} {_stringLocalizer[SharedResourcesKeys.NotFound]}");
			// call delete service
			var result = await _attendeeService.DeleteAsync(attendee);
			if (result == "Success")
				return Success<string>($"{_stringLocalizer[SharedResourcesKeys.Updated]}");
			else
				return BadRequest<string>($"{_stringLocalizer[SharedResourcesKeys.FailedToUpdate]}");
		}

		public async Task<Response<string>> Handle(ChangeRSVPStatusCommand request, CancellationToken cancellationToken)
		{
			var attendee = await _attendeeService.GetAttendeeByUserIdEventIdAsync(request.userId, request.eventId);

			// handle RSVPDate
			if (Enum.TryParse(typeof(RSVPStatus), request.status, true, out var statusParsing))
			{
				if ((RSVPStatus)statusParsing != attendee.Status)
				{
					attendee.RSVPDate = DateTime.UtcNow;
					attendee.Status = (RSVPStatus)statusParsing;
				}

			}

			// call update attendee service
			var result = await _attendeeService.UpdateAsyc(attendee);
			if (result != "Success")
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);

			return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);

		}

		public async Task<Response<string>> Handle(MarkAttendanceCommand request, CancellationToken cancellationToken)
		{
			var attendee = await _attendeeService.GetAttendeeByUserIdEventIdAsync(request.userId, request.eventId);
			// Mark as attended
			attendee.HasAttended = true;
			// call update attendee service
			var result = await _attendeeService.UpdateAsyc(attendee);
			if (result != "Success")
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);

			return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);

		}
		#endregion



	}
}
