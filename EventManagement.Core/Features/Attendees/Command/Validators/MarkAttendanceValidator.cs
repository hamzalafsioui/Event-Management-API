using EventManagement.Core.Features.Attendees.Command.Models;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Core.Features.Attendees.Command.Validators
{
	public class MarkAttendanceValidator:AbstractValidator<MarkAttendanceCommand>
	{
		#region Fields
		private readonly IAttendeeService _attendeeService;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructors
		public MarkAttendanceValidator(IAttendeeService attendeeService, IStringLocalizer<SharedResources> stringLocalizer)
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

			RuleFor(x => x.eventId)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

			RuleFor(x => x.userId)
			.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);




		}

		public void ApplyCustomValidationsRules()
		{
			// is Attendee Exist 
			RuleFor(x => x.userId)
				.MustAsync(async (context, key, CancellationToken) =>
				{
					var user = await _attendeeService.GetAttendeeByUserIdEventIdAsync(context.userId, context.eventId);
					return user != null;
				})
				.WithMessage(_stringLocalizer[SharedResourcesKeys.NotFound]);




		}
		#endregion
	}
}
