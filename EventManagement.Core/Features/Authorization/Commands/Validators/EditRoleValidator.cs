using EventManagement.Core.Features.Authorization.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Authorization.Commands.Validators
{
	public class EditRoleValidator:AbstractValidator<EditRoleCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IAuthorizationService _authorizationService;

		#endregion
		#region Constructors
		public EditRoleValidator(IStringLocalizer<SharedResources> stringLocalizer, IAuthorizationService authorizationService)
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
			RuleFor(x => x.Id)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

		}
		private void ApplyCustomValidationsRules()
		{
			RuleFor(x => x.Id)
				.MustAsync(async (key, CancellationToken) => await _authorizationService.IsRoleExistByIdAsync(key))
				.WithMessage(_stringLocalizer[SharedResourcesKeys.RoleId] +" "+_stringLocalizer[SharedResourcesKeys.NotFound]);
		}


		#endregion
	}
}
