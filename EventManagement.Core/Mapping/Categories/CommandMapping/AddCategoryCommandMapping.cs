using EventManagement.Core.Features.Categories.Command.Models;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Categories
{
	public partial class CategoryProfile
	{
		private void AddCategoryCommandMapping()
		{
			CreateMap<AddCategoryCommand, Category>();
		}
	}
}
