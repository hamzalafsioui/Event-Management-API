using EventManagement.Core.Features.Categories.Command.Models;
using EventManagement.Core.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Categories.Command.Validators
{
	public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
	{
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;

		public DeleteCategoryValidator(IStringLocalizer<SharedResources> stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;

			RuleFor(x => x.CategoryId)
				.Must(x => x > 0).WithMessage(_stringLocalizer[SharedResourcesKeys.InvalidId]);
		}
	}
}
