using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Users.Commands.Validatiors
{
	public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
	{
		#region Fields
		private readonly UserManager<User> _userManager;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;

		#endregion
		#region Constructors
		public ChangeUserPasswordValidator(UserManager<User> userManager, IStringLocalizer<SharedResources> stringLocalizer)
		{
			this._userManager = userManager;
			this._stringLocalizer = stringLocalizer;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.Id)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

			RuleFor(x => x.CurrentPassword)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

			RuleFor(x => x.CurrentPassword)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
				.MinimumLength(3);

			RuleFor(x => x.ConfirmPassword)
				.Equal(x => x.NewPassword)
				.WithMessage($"{_stringLocalizer[SharedResourcesKeys.Password]} " + _stringLocalizer[SharedResourcesKeys.NotMatched]);

		}

		public void ApplyCustomValidationsRules()
		{

		}
		#endregion
	}
}
