using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Categories.Command.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Categories.Command.Handlers
{
	public class CategoryCommandHandler : ResponseHandler,
		IRequestHandler<AddCategoryCommand, Response<string>>

	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;

		#endregion
		#region Constructors
		public CategoryCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, ICategoryService categoryService,
			IMapper mapper) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_categoryService = categoryService;
			_mapper = mapper;
		}
		#endregion
		#region Handle Functions
		public async Task<Response<string>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
		{
			// check is category exist
			var Found = await _categoryService.IsCategoryNameExistAsync(request.Name);
			if (Found)
				return BadRequest<string>();
			// mapping
			var categoryMapping = _mapper.Map<Category>(request);
			// save data
			var result = await _categoryService.AddAsync(categoryMapping);
			if (result == "Success")
				return Success<string>(_stringLocalizer[SharedResourcesKeys.Created]);
			else
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAdd]);

		}
		#endregion



	}
}
