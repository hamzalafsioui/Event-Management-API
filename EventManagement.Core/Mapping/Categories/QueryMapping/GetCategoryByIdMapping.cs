using EventManagement.Core.Features.Categories.Queries.Responses;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Categories
{
	public partial class CategoryProfile
	{
		private void GetCategoryByIdMapping()
		{
			CreateMap<Category, GetCategoryByIdResponse>();
		}
	}
}
