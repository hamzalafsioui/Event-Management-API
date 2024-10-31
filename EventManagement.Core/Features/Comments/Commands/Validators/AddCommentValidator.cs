using EventManagement.Core.Features.Comments.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Comments.Commands.Validators
{
	public class AddCommentValidator : AbstractValidator<AddCommentCommand>
	{
		#region Fields
		private readonly IAttendeeService _attendeeService;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;

		#endregion
		#region Constructors
		public AddCommentValidator(IAttendeeService attendeeService, IStringLocalizer<SharedResources> stringLocalizer)
		{
			_attendeeService = attendeeService;
			_stringLocalizer = stringLocalizer;
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


			RuleFor(x => x.content)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);


		}

		public void ApplyCustomValidationsRules()
		{
			// is event exist
			RuleFor(x => x.eventId)
				.MustAsync((context, key, CancellationToken) => _attendeeService.IsUserAttendedEvent(key, context.userId))
				.WithMessage(_stringLocalizer[SharedResourcesKeys.UserId] + " " + _stringLocalizer[SharedResourcesKeys.NotAttended]);

		}
		#endregion
	}
}
