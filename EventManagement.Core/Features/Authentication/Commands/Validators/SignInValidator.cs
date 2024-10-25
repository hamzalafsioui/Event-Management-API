using EventManagement.Core.Features.Authentication.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Authentication.Commands.Validators
{
	public class SignInValidator : AbstractValidator<SignInCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly UserManager<User> _userManager;

		#endregion

		#region Constructors
		public SignInValidator(IStringLocalizer<SharedResources> stringLocalizer, UserManager<User> userManager)
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
			RuleFor(x => x.UserName)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
				.MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLengthIs100])
				.MinimumLength(4);

			RuleFor(x => x.Password)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

		}

		public void ApplyCustomValidationsRules()
		{
			//	RuleFor(x => x.UserName)
			//		.MustAsync(async (username, cancellationToken) =>
			//		{
			//			var user = await _userManager.FindByNameAsync(username);
			//			return user == null;
			//		})
			//		.WithMessage($"{_stringLocalizer[SharedResourcesKeys.UsernameAlreadyExist]}");
			//}
			#endregion
		}
	}
}
