using EventManagement.Core.Features.Events.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Events.Commands.Validatiors
{
	public class EditEventValidator : AbstractValidator<EditEventCommand>
	{
		#region Fields
		private readonly IEventService _eventService;
		private readonly ICategoryService _categoryService;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;

		#endregion
		#region Constructors
		public EditEventValidator(IEventService eventService, ICategoryService categoryService, IStringLocalizer<SharedResources> stringLocalizer)
		{
			_eventService = eventService;
			_categoryService = categoryService;
			_stringLocalizer = stringLocalizer;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.Title)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
				.MaximumLength(50).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLengthIs100])
				.MinimumLength(4);

			RuleFor(x => x.Location)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);


			RuleFor(x => x.StartTime)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

			RuleFor(x => x.EndTime)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

			RuleFor(x => x.CategoryId)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

			RuleFor(x => x.CreatorId)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

		}

		public void ApplyCustomValidationsRules()
		{
			// is CategoryId Exist
			RuleFor(x => x.CategoryId)
				.MustAsync((key, CancellationToken) => _categoryService.IsCategoryIdExist(key))
				.WithMessage(_stringLocalizer[SharedResourcesKeys.CategoryId] + " " + _stringLocalizer[SharedResourcesKeys.NotFound]);

			// is EndTime after StartTime
			RuleFor(x => x.EndTime)
				.Must((context, endTime, CancellationToken) =>
				{
					var startTime = context.StartTime;
					return startTime < endTime;
				}).WithMessage(_stringLocalizer[$"{_stringLocalizer[SharedResourcesKeys.EndTime]} {_stringLocalizer[SharedResourcesKeys.MustBeAfter]} {_stringLocalizer[SharedResourcesKeys.StartTime]}"]);

			// check is an exist conflict StartTime & EndTime with Other Event
		}
		#endregion
	}
}
