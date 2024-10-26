using EventManagement.Data.Entities;

namespace EventManagement.Service.Abstracts
{
	public interface ICategoryService
	{
		public Task<bool> IsCategoryIdExist(int categoryId);
		public Task<List<Category>> GetCategoriesListAsync();
		public Task<Category> GetCategoryByIdAsync(int categoryId);
	}
}
