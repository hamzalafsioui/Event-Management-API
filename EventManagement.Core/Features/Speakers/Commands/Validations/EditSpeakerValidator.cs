using EventManagement.Core.Features.Speakers.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities.Identity;
using EventManagement.Service.Abstracts;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Speakers.Commands.Validations
{

	public class EditSpeakerValidator : AbstractValidator<EditSpeakerCommand>
	{
		#region Fields
		private readonly UserManager<User> _userManager;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly ISpeakerService _speakerService;

		#endregion
		#region Constructors
		public EditSpeakerValidator(UserManager<User> userManager,
			IStringLocalizer<SharedResources> stringLocalizer,
			ISpeakerService speakerService)
		{
			_userManager = userManager;
			_stringLocalizer = stringLocalizer;
			_speakerService = speakerService;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.SpeakerId)
			.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

			RuleFor(x => x.Bio)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
				.MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLengthIs100])
				.MinimumLength(4);

		}

		public void ApplyCustomValidationsRules()
		{
			RuleFor(x => x.SpeakerId)
				.MustAsync(async (key, cancellationToken) =>
				{
					var speaker = await _speakerService.GetByIdAsync(key);
					return speaker != null;

				})
				.WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotFound]}");





		}
		#endregion
	}

}
