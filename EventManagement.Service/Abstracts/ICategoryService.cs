using EventManagement.Data.Entities;

namespace EventManagement.Service.Abstracts
{
	public interface ICategoryService
	{
		public Task<bool> IsCategoryIdExist(int categoryId);
		public Task<List<Category>> GetCategoriesListAsync();
		public Task<Category> GetCategoryByIdAsync(int categoryId);
		public Task<bool> IsCategoryNameExistAsync(string categoryName);
		public Task<bool> IsCategoryNameExistExcludeSelfAsync(string categoryName, int categoryId);
		public Task<string> AddAsync(Category category);
		public Task<string> EditAsync(Category category);
	}
}
