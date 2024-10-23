using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

public class AddUserValidator : AbstractValidator<AddUserCommand>
{
	#region Fields
	private readonly UserManager<User> _userManager;
	private readonly IStringLocalizer<SharedResources> _stringLocalizer;

	#endregion
	#region Constructors
	public AddUserValidator(UserManager<User> userManager, IStringLocalizer<SharedResources> stringLocalizer)
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
		RuleFor(x => x.UserName)
			.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
			.MaximumLength(15).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLengthIs100])
			.MinimumLength(4);

		RuleFor(x => x.Email)
			.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
			.Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage(_stringLocalizer[SharedResourcesKeys.InvalidFormat]);

		RuleFor(x => x.FirstName)
			.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

		RuleFor(x => x.LastName)
			.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

		RuleFor(x => x.Password)
			.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
			.MinimumLength(3);

		RuleFor(x => x.ConfirmPassword)
			.Equal(x => x.Password)
			.WithMessage($"{_stringLocalizer[SharedResourcesKeys.Password]} " + _stringLocalizer[SharedResourcesKeys.NotMatched]);
	}

	public void ApplyCustomValidationsRules()
	{
		RuleFor(x => x.UserName)
			.MustAsync(async (username, cancellationToken) =>
			{
				var user = await _userManager.FindByNameAsync(username);
				return user == null;
			})
			.WithMessage($"{_stringLocalizer[SharedResourcesKeys.UsernameAlreadyExist]}");
	}
	#endregion
}
