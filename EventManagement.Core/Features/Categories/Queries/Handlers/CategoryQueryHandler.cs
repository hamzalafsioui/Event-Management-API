using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Categories.Queries.Models;
using EventManagement.Core.Features.Categories.Queries.Responses;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace EventManagement.Core.Features.Categories.Queries.Handlers
{
	public class CategoryQueryHandler : ResponseHandler,
		IRequestHandler<GetCategoryListQuery, Response<List<GetCategoryListResponse>>>
	{
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;
		#region Fields

		#endregion
		#region Constructors
		public CategoryQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, ICategoryService categoryService,
			IMapper mapper) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_categoryService = categoryService;
			this._mapper = mapper;
		}


		#endregion
		#region Handle Functions
		public async Task<Response<List<GetCategoryListResponse>>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
		{
			// call service to retrieve categories list
			var CategoriesList = await _categoryService.GetCategoriesListAsync();
			// mapping 
			var categoriesListMapping = _mapper.Map<List<GetCategoryListResponse>>(CategoriesList);

			// return success
			var result = Success(categoriesListMapping);
			result.Meta = new
			{
				Count = categoriesListMapping.Count,
			};
			return result;
		}
		#endregion

	}
}
