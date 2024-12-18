﻿using EventManagement.Core.Features.Attendees.Command.Models;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Attendees.Command.Validators
{
	public class LeaveEventValidator : AbstractValidator<LeaveEventCommand>
	{
		#region Fields
		private readonly IAttendeeService _attendeeService;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructors
		public LeaveEventValidator(IAttendeeService attendeeService, IStringLocalizer<SharedResources> stringLocalizer)
		{
			_attendeeService = attendeeService;
			this._stringLocalizer = stringLocalizer;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{

			RuleFor(x => x.EventId)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

			RuleFor(x => x.UserId)
			.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);




		}

		public void ApplyCustomValidationsRules()
		{
			// is Attendee Exist 
			RuleFor(x => x.UserId)
				.MustAsync(async (context, key, CancellationToken) =>
				{
					var user = await _attendeeService.GetAttendeeByUserIdEventIdAsync(context.UserId, context.EventId);
					return user != null;
				})
				.WithMessage(_stringLocalizer[SharedResourcesKeys.NotFound]);




		}
		#endregion
	}
}
