using EventManagement.Core.Features.Emails.Commands.Models;
using EventManagement.Core.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Emails.Commands.Validators
{
	public class SendEmailValidator : AbstractValidator<SendEmailCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;

		#endregion
		#region Constructors
		public SendEmailValidator(IStringLocalizer<SharedResources> stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			ApplyValidationsRules();
			ApplyCustomsRules();
		}
		#endregion
		#region Hnadle Functions
		private void ApplyValidationsRules()
		{
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
			RuleFor(x => x.Message)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

		}
		private void ApplyCustomsRules()
		{

		}
		#endregion
	}
}
