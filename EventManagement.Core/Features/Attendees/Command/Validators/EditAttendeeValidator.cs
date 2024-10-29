using EventManagement.Core.Features.Attendees.Command.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities.Identity;
using EventManagement.Service.Abstracts;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Attendees.Command.Validators
{
	public class EditAttendeeValidator : AbstractValidator<EditAttendeeCommand>
	{
		#region Fields
		private readonly IAttendeeService _attendeeService;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly UserManager<User> _userManager;
		private readonly IEventService _eventService;

		#endregion
		#region Constructors
		public EditAttendeeValidator(IAttendeeService attendeeService, IStringLocalizer<SharedResources> stringLocalizer,
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

			RuleFor(x => x.HasAttended)
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

			// block operation HasAttended if date > EndTime 


		}
		#endregion
	}
}
