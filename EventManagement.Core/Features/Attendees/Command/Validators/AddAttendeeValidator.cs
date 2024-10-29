using EventManagement.Core.Features.Attendees.Command.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities.Identity;
using EventManagement.Service.Abstracts;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Attendees.Command.Validators
{
	public class AddAttendeeValidator : AbstractValidator<AddAttendeeCommand>
	{
		#region Fields
		private readonly IAttendeeService _attendeeService;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly UserManager<User> _userManager;
		private readonly IEventService _eventService;

		#endregion
		#region Constructors
		public AddAttendeeValidator(IAttendeeService attendeeService, IStringLocalizer<SharedResources> stringLocalizer,
			UserManager<User> userManager, IEventService eventService)
		{
			_attendeeService = attendeeService;
			this._stringLocalizer = stringLocalizer;
			this._userManager = userManager;
			this._eventService = eventService;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.UserId)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);


			RuleFor(x => x.EventId)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);


			RuleFor(x => x.Status)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);


		}

		public void ApplyCustomValidationsRules()
		{
			// is user Exist 
			RuleFor(x => x.UserId)
				.MustAsync(async (key, CancellationToken) =>
				{
					var user = await _userManager.FindByIdAsync(key.ToString());
					return user != null;
				})
				.WithMessage(_stringLocalizer[SharedResourcesKeys.UserId] + " " + _stringLocalizer[SharedResourcesKeys.NotFound]);

			// is Event Exist 
			RuleFor(x => x.EventId)
				.MustAsync(async (key, CancellationToken) =>
				{
					var user = await _eventService.GetEventByIdAsync(key);
					return user != null;
				})
				.WithMessage(_stringLocalizer[SharedResourcesKeys.EventId] + " " + _stringLocalizer[SharedResourcesKeys.NotFound]);

			// Is User attended to event
			RuleFor(x => x.UserId)
				.MustAsync(async (context, key, CancellationToken) =>
				{
					var attendee = await _attendeeService.GetAttendeeByUserIdEventIdAsync(key, context.EventId);
					return attendee == null;
				}).WithMessage(_stringLocalizer[SharedResourcesKeys.AlreadyRegistered]);

			// check is date < startTime of the event



		}
		#endregion

	}
}
