using EventManagement.Data.Entities;

namespace EventManagement.Service.Abstracts
{
	public interface ICategoryService
	{
		public Task<bool> IsCategoryIdExist(int categoryId);
		public Task<List<Category>> GetCategoriesListAsync();
		public Task<Category?> GetCategoryByIdAsync(int categoryId);
		public Task<bool> IsCategoryNameExistAsync(string categoryName);
		public Task<bool> IsCategoryNameExistExcludeSelfAsync(string categoryName, int categoryId);
		public Task<Category> AddAsync(Category category);
		public Task<Category> EditAsync(Category category);
		public Task<bool> DeleteAsync(Category category);
	}
}
