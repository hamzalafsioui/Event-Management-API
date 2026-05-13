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
		IRequestHandler<MarkAttendanceCommand, Response<string>>
	{
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IAttendeeService _attendeeService;
		private readonly IMapper _mapper;
		private readonly IEventService _eventService;
		private readonly IUserService _userService;
		private readonly IEmailService _emailService;
		#region Fields

		#endregion
		#region Consturctors
		public AttendeeCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IAttendeeService attendeeService,
			IMapper mapper, IEventService eventService, IUserService userService, IEmailService emailService) : base(stringLocalizer)
		{
			this._stringLocalizer = stringLocalizer;
			_attendeeService = attendeeService;
			this._mapper = mapper;
			_eventService = eventService;
			_userService = userService;
			_emailService = emailService;
		}
		#endregion
		#region Handle Functions
		public async Task<Response<string>> Handle(AddAttendeeCommand request, CancellationToken cancellationToken)
		{
			// mapping 
			var attendeeMapping = _mapper.Map<Attendee>(request);

			// check capacity for waitlist
			if (attendeeMapping.Status == RSVPStatus.Going)
			{
				var eventDetails = await _eventService.GetEventByIdAsync(request.EventId);
				if (eventDetails != null)
				{
					int goingCount = await _attendeeService.GetGoingAttendeesCountAsync(request.EventId);
					if (goingCount >= eventDetails.Capacity)
					{
						attendeeMapping.Status = RSVPStatus.Waitlisted;
					}
				}
			}

			// call add attendee service
			var newAttendee = await _attendeeService.AddAsync(attendeeMapping);
			if (newAttendee == null)
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAdd]);

			return Created<string>(_stringLocalizer[SharedResourcesKeys.Created]);
		}

		public async Task<Response<string>> Handle(EditAttendeeCommand request, CancellationToken cancellationToken)
		{
			var attendee = await _attendeeService.GetAttendeeByUserIdEventIdAsync(request.UserId, request.EventId);

			// mapping 
			var attendeeMapping = _mapper.Map<Attendee>(request);
			var previousStatus = attendee!.Status;

			// handle capacity if changed to Going
			if (attendeeMapping.Status == RSVPStatus.Going && previousStatus != RSVPStatus.Going)
			{
				var eventDetails = await _eventService.GetEventByIdAsync(request.EventId);
				if (eventDetails != null)
				{
					int goingCount = await _attendeeService.GetGoingAttendeesCountAsync(request.EventId);
					if (goingCount >= eventDetails.Capacity)
					{
						attendeeMapping.Status = RSVPStatus.Waitlisted;
					}
				}
			}

			// handle RSVPDate
			if (attendeeMapping.Status != previousStatus)
				attendeeMapping.RSVPDate = DateTime.UtcNow;
			else
				attendeeMapping.RSVPDate = attendee.RSVPDate;
			// call update attendee service
			var result = await _attendeeService.UpdateAsyc(attendeeMapping);
			if (result == null)
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);

			if (previousStatus == RSVPStatus.Going && attendeeMapping.Status != RSVPStatus.Going)
			{
				await HandleWaitlistPromotionAsync(request.EventId);
			}

			return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
		}

		public async Task<Response<string>> Handle(LeaveEventCommand request, CancellationToken cancellationToken)
		{
			// check is event exist
			var attendee = await _attendeeService.GetAttendeeByUserIdEventIdAsync(request.UserId, request.EventId);
			// return BadRequest if not exist
			if (attendee == null)
				return NotFound<string>($"{_stringLocalizer[SharedResourcesKeys.EventId]} {request.EventId} {_stringLocalizer[SharedResourcesKeys.NotFound]}");
			var previousStatus = attendee.Status;

			// call delete service
			var result = await _attendeeService.DeleteAsync(attendee);
			if (result)
			{
				if (previousStatus == RSVPStatus.Going)
				{
					await HandleWaitlistPromotionAsync(request.EventId);
				}
				return Success<string>($"{_stringLocalizer[SharedResourcesKeys.Updated]}");
			}
			else
				return BadRequest<string>($"{_stringLocalizer[SharedResourcesKeys.FailedToUpdate]}");
		}

		public async Task<Response<string>> Handle(ChangeRSVPStatusCommand request, CancellationToken cancellationToken)
		{
			var attendee = await _attendeeService.GetAttendeeByUserIdEventIdAsync(request.userId, request.eventId);
			// If the attendee doesn't exist, return NotFound
			if (attendee == null)
				return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);

			// handle RSVPDate
			if (Enum.TryParse(typeof(RSVPStatus), request.status, true, out var statusParsing))
			{
				var previousStatus = attendee.Status;
				var newStatus = (RSVPStatus)statusParsing;

				if (newStatus != previousStatus)
				{
					if (newStatus == RSVPStatus.Going)
					{
						var eventDetails = await _eventService.GetEventByIdAsync(request.eventId);
						if (eventDetails != null)
						{
							int goingCount = await _attendeeService.GetGoingAttendeesCountAsync(request.eventId);
							if (goingCount >= eventDetails.Capacity)
							{
								newStatus = RSVPStatus.Waitlisted;
							}
						}
					}

					attendee.RSVPDate = DateTime.UtcNow;
					attendee.Status = newStatus;
				}

				var resultUpdate = await _attendeeService.UpdateAsyc(attendee);
				if (resultUpdate == null)
					return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);

				if (previousStatus == RSVPStatus.Going && newStatus != RSVPStatus.Going)
				{
					await HandleWaitlistPromotionAsync(request.eventId);
				}

				return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
			}

			// call update attendee service
			var result = await _attendeeService.UpdateAsyc(attendee);
			if (result == null)
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);

			return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);

		}

		public async Task<Response<string>> Handle(MarkAttendanceCommand request, CancellationToken cancellationToken)
		{
			var attendee = await _attendeeService.GetAttendeeByUserIdEventIdAsync(request.userId, request.eventId);
			// If the attendee doesn't exist, return NotFound
			if (attendee == null)
				return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			// Mark as attended
			attendee.HasAttended = true;
			// call update attendee service
			var result = await _attendeeService.UpdateAsyc(attendee);
			if (result == null)
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);

			return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);

		}
		private async Task HandleWaitlistPromotionAsync(int eventId)
		{
			var waitlistedAttendees = await _attendeeService.GetAllWaitlistedAttendeesAsync(eventId);
			if (waitlistedAttendees.Any())
			{
				var eventDetails = await _eventService.GetEventByIdAsync(eventId);
				if (eventDetails != null)
				{
					string subject = $"A spot has opened up for {eventDetails.Title}!";
					string message = $"<p>Great news! A spot has opened up for <strong>{eventDetails.Title}</strong>.</p>" +
									 $"<p>Please log in to the application and manually enroll (change your RSVP status to 'Going') to secure your spot. It is on a first-come, first-served basis!</p>";

					foreach (var attendee in waitlistedAttendees)
					{
						var user = await _userService.GetByIdAsync(attendee.UserId);
						if (user != null)
						{
							await _emailService.SendEmailAsync(user.Email, message, subject);
						}
					}
				}
			}
		}
		#endregion



	}
}
