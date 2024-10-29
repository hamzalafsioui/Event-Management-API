using EventManagement.Core.Features.Events.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Events.Commands.Validatiors
{
	public class CancelEventValidator : AbstractValidator<CancelEventCommand>
	{
		#region Fields
		private readonly IEventService _eventService;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;

		#endregion
		#region Constructors
		public CancelEventValidator(IEventService eventService, IStringLocalizer<SharedResources> stringLocalizer)
		{
			_eventService = eventService;
			_stringLocalizer = stringLocalizer;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.EventId)
				.Must(x => x > 0)
				.WithMessage(_stringLocalizer[SharedResourcesKeys.InvalidId]);



		}

		public void ApplyCustomValidationsRules()
		{
			// is Event Exist
			RuleFor(x => x.EventId)
				.MustAsync(async (key, CancellationToken) =>
				{
					var @event = await _eventService.GetEventByIdAsync(key);
					return @event != null;
				}).WithMessage(_stringLocalizer[$"{_stringLocalizer[SharedResourcesKeys.EventId]} {_stringLocalizer[SharedResourcesKeys.NotFound]}"]);


		}
		#endregion
	}
}
