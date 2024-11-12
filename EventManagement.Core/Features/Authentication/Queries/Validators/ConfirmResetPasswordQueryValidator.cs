using EventManagement.Core.Features.Authentication.Queries.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Authentication.Queries.Validators
{
	public class ConfirmResetPasswordQueryValidator : AbstractValidator<ConfirmResetPasswordQuery>
	{
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly UserManager<User> _userManager;
		#region Fields

		#endregion
		#region Constructors
		public ConfirmResetPasswordQueryValidator(IStringLocalizer<SharedResources> stringLocalizer, UserManager<User> userManager)
		{
			this._stringLocalizer = stringLocalizer;
			_userManager = userManager;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}


		#endregion
		#region Handle Functions
		private void ApplyValidationsRules()
		{
			RuleFor(x => x.Code)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
			RuleFor(x => x.Email)
					.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
					.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
		}
		private void ApplyCustomValidationsRules()
		{

		}

		#endregion
	}
}
