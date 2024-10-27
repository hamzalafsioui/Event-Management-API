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
		IRequestHandler<AddCategoryCommand, Response<string>>,
		IRequestHandler<EditCategoryCommand, Response<string>>,
		IRequestHandler<DeleteCategoryCommand, Response<string>>

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
				return Created<string>(_stringLocalizer[SharedResourcesKeys.Created]);
			else
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAdd]);

		}

		public async Task<Response<string>> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
		{
			// check is category Exist
			var category = await _categoryService.GetCategoryByIdAsync(request.CategoryId);
			// not found
			if (category == null)
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			// check is categoryName exist
			var IscategoryNameExist = await _categoryService.IsCategoryNameExistExcludeSelfAsync(request.Name, request.CategoryId);
			if (IscategoryNameExist)
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.CategoryExist]);
			// mapping
			var categoryMapping = _mapper.Map<Category>(request);
			// call Edit service
			var result = await _categoryService.EditAsync(categoryMapping);
			if (result != "Success")
				BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);

			return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);

		}

		public async Task<Response<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
		{
			//retrieve category
			var category = await _categoryService.GetCategoryByIdAsync(request.CategoryId);
			// if not exist 
			if (category == null)
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);

			// call delete service
			var result = await _categoryService.DeleteAsync(category);
			if (result != "Success")
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToDelete]);

			return Deleted<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
		}
		#endregion



	}
}
