using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Users.Commands.Validatiors
{
	public class EditUserValidator : AbstractValidator<EditUserCommand>
	{
		#region Fields
		private readonly UserManager<User> _userManager;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;

		#endregion
		#region Constructors
		public EditUserValidator(UserManager<User> userManger, IStringLocalizer<SharedResources> stringLocalizer)
		{
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
			this._userManager = userManger;
			this._stringLocalizer = stringLocalizer;
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.Username)
				.NotEmpty().WithMessage("{PropertyName} is required, Must not be empty")
				.NotNull().WithMessage("{PropertyName} with {PropertyValue} Must not be Null")
				.MaximumLength(15).WithMessage("{PropertyName} Max length is 15")
				.MinimumLength(4);

			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("{PropertyName} is required, Must not be empty")
				.NotNull().WithMessage("{PropertyName} Must not be Null")
				.Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("{PropertyName} is not in a valid format");

			RuleFor(x => x.FirstName)
				.NotEmpty().WithMessage("{PropertyName} is required, Must not be empty")
				.NotNull().WithMessage("{PropertyName} with {PropertyValue} Must not be Null");

			RuleFor(x => x.LastName)
				.NotEmpty().WithMessage("{PropertyName} is required, Must not be empty")
				.NotNull().WithMessage("{PropertyName} with {PropertyValue} Must not be Null");

		}

		public void ApplyCustomValidationsRules()
		{
			RuleFor(x => x.Username)
				.MustAsync(async (model, key, CancellationToken) =>
				{
					// Excluded current user
					var user = await _userManager.FindByNameAsync(key);
					return user == null || user.Id == model.Id;
				})
				.WithMessage($"{_stringLocalizer[SharedResourcesKeys.UsernameAlreadyExist]}");
		}
		#endregion
	}
}
