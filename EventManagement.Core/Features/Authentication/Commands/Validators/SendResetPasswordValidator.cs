using EventManagement.Core.Features.Authentication.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Authentication.Commands.Validators
{
	public class SendResetPasswordValidator : AbstractValidator<SendResetPasswordCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly UserManager<User> _userManager;

		#endregion

		#region Constructors
		public SendResetPasswordValidator(IStringLocalizer<SharedResources> stringLocalizer, UserManager<User> userManager)
		{
			this._stringLocalizer = stringLocalizer;
			this._userManager = userManager;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

		}

		public void ApplyCustomValidationsRules()
		{
			RuleFor(x => x.Email)
				.MustAsync(async (key, cancellationToken) =>
				{
					var user = await _userManager.FindByEmailAsync(key);
					return user != null;
				})
				.WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotFound]}");
		}
		#endregion
	}

}
