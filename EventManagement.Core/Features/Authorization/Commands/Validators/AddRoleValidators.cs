using EventManagement.Core.Features.Authorization.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Authorization.Commands.Validators
{
	public class AddRoleValidators : AbstractValidator<AddRoleCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IAuthorizationService _authorizationService;

		#endregion
		#region Constructors
		public AddRoleValidators(IStringLocalizer<SharedResources> stringLocalizer, IAuthorizationService authorizationService)
		{
			this._stringLocalizer = stringLocalizer;
			_authorizationService = authorizationService;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}


		#endregion
		#region Handle Functions
		private void ApplyValidationsRules()
		{
			RuleFor(x => x.RoleName)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

		}
		private void ApplyCustomValidationsRules()
		{
			RuleFor(x => x.RoleName)
				.MustAsync(async (key,CancellationToken) => ! await _authorizationService.IsRoleExistAsync(key))
				.WithMessage(_stringLocalizer[SharedResourcesKeys.AlreadyRegistered]);
		}


		#endregion


	}
}
