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
		IRequestHandler<EditAttendeeCommand, Response<string>>
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
			// mapping 
			var attendeeMapping = _mapper.Map<Attendee>(request);
			// call add attendee service
			var result = await _attendeeService.UpdateAsyc(attendeeMapping);
			if (result != "Success")
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);

			return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
		}
		#endregion



	}
}
