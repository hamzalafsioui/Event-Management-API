using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Users.Commands.Validatiors
{
	public class AddUserValidator : AbstractValidator<AddUserCommand>
	{
		#region Fields
		private readonly IUserService _userService;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;

		#endregion
		#region Constructors
		public AddUserValidator(IUserService userService, IStringLocalizer<SharedResources> stringLocalizer)
		{
			this._userService = userService;
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
				.MustAsync(async (key, CancellationToken) => !(await _userService.IsUserNameExist(key)))
				.WithMessage($"{_stringLocalizer[SharedResourcesKeys.Username]} " + _stringLocalizer[SharedResourcesKeys.AlreadyExist]);
		}
		#endregion
	}
}
