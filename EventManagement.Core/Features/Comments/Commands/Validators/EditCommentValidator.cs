using EventManagement.Core.Features.Comments.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Comments.Commands.Validators
{
	public class EditCommentValidator : AbstractValidator<EditCommentCommand>
	{
		#region Fields
		private readonly ICommentService _commentService;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;

		#endregion
		#region Constructors
		public EditCommentValidator(ICommentService commentService, IStringLocalizer<SharedResources> stringLocalizer)
		{
			_commentService = commentService;
			_stringLocalizer = stringLocalizer;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.commentId)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

			RuleFor(x => x.content)
				.NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);


		}

		public void ApplyCustomValidationsRules()
		{
			// invalid Id
			RuleFor(x => x.commentId)
				.Must((context,key, CancellationToken) => context.commentId>0)
				.WithMessage(_stringLocalizer[SharedResourcesKeys.InvalidId]);

			// is event exist
			RuleFor(x => x.commentId)
				.MustAsync(async ( key, CancellationToken) => await _commentService.IsCommentExistByIdAsync(key))
				.WithMessage(_stringLocalizer[SharedResourcesKeys.NotFound]);

		}
		#endregion
	}
}
