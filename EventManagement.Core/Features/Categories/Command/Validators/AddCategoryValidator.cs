using EventManagement.Core.Features.Categories.Command.Models;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Categories.Command.Validators
{
	public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
	{
		#region Fields

		private readonly ICategoryService _categoryService;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;

		#endregion
		#region Constructors
		public AddCategoryValidator(ICategoryService categoryService, IStringLocalizer<SharedResources> stringLocalizer)
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
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
				.MaximumLength(50);
		}

		public void ApplyCustomValidationsRules()
		{
			// is CategoryName Exist (this step is already exist in categoryCommandHandler)
			RuleFor(x => x.Name)
				.MustAsync(async (key, CancellationToken) => !(await _categoryService.IsCategoryNameExistAsync(key)))
				.WithMessage(_stringLocalizer[SharedResourcesKeys.CategoryExist]);

		}
		#endregion
	}
}
