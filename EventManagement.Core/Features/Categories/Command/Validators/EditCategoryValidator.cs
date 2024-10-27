using EventManagement.Core.Features.Categories.Command.Models;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Categories.Command.Validators
{
	public class EditCategoryValidator : AbstractValidator<EditCategoryCommand>
	{
		#region Fields

		private readonly ICategoryService _categoryService;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;

		#endregion
		#region Constructors
		public EditCategoryValidator(ICategoryService categoryService, IStringLocalizer<SharedResources> stringLocalizer)
		{
			_categoryService = categoryService;
			_stringLocalizer = stringLocalizer;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.CategoryId)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
				.Must(x => x > 0).WithMessage(_stringLocalizer[SharedResourcesKeys.InvalidId]);

			RuleFor(x => x.Name)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
				.MaximumLength(50);
		}

		public void ApplyCustomValidationsRules()
		{
			// is CategoryName Exist Exclude Self (this step is already exist in categoryCommandHandler)
			RuleFor(x => x.Name)
				.MustAsync(async (context, key, CancellationToken) => !(await _categoryService.IsCategoryNameExistExcludeSelfAsync(key, context.CategoryId)))
				.WithMessage(_stringLocalizer[SharedResourcesKeys.CategoryExist]);

		}
		#endregion
	}
}
