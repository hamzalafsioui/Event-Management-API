using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Service.Abstracts;
using FluentValidation;

namespace EventManagement.Core.Features.Users.Commands.Validatiors
{
	public class EditUserValidator:AbstractValidator<EditUserCommand>
	{
		#region Fields
		private readonly IUserService _userService;

		#endregion
		#region Constructors
		public EditUserValidator(IUserService userService)
		{
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
			this._userService = userService;
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

			RuleFor(x => x.PasswordHash)
				.NotEmpty().WithMessage("{PropertyName} is required, Must not be empty")
				.NotNull().WithMessage("{PropertyName} with {PropertyValue} Must not be Null")
				.MinimumLength(3);

		}

		public void ApplyCustomValidationsRules()
		{
			RuleFor(x => x.Username)
				.MustAsync(async (model,key,CancellationToken) => !(await _userService.IsUserNameExistExcludeSelf(key,model.UserId)))
				.WithMessage("UserName Is Already Exist");
		}
		#endregion
	}
}
