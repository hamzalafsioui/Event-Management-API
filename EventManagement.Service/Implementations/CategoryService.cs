using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Repositories;
using EventManagement.Service.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Service.Implementations
{
	public class CategoryService : ICategoryService
	{
		#region Fields
		private readonly ICategoryRepository _categoryRepository;

		#endregion

		#region Constructors
		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}


		#endregion
		#region Handle Functions
		public async Task<bool> IsCategoryIdExist(int categoryId)
		{
			var result = await _categoryRepository.GetTableNoTracking().AnyAsync(c => c.CategoryId.Equals(categoryId));
			return result;
		}
		public async Task<List<Category>> GetCategoriesListAsync()
		{
			var result = await _categoryRepository.GetTableNoTracking().ToListAsync();
			return result;
		}

		public async Task<Category> GetCategoryByIdAsync(int categoryId)
		{
			var category = await _categoryRepository.GetTableNoTracking().Where(x => x.CategoryId.Equals(categoryId)).FirstOrDefaultAsync();
			return category!;
		}
		public async Task<bool> IsCategoryNameExistAsync(string name)
		{
			var category = await _categoryRepository.GetTableNoTracking().Where(c => c.Name.Equals(name)).FirstOrDefaultAsync();
			if (category == null)
				return false;
			else
				return true;

		}
		public async Task<bool> IsCategoryNameExistExcludeSelfAsync(string categoryName, int categoryId)
		{
			var category = await _categoryRepository.GetTableNoTracking().Where(c => c.Name.Equals(categoryName) && c.CategoryId != categoryId).FirstOrDefaultAsync();
			if (category == null)
				return false;
			else
				return true;

		}
		public async Task<string> AddAsync(Category category)
		{
			await _categoryRepository.AddAsync(category);
			return "Success";
		}

		public async Task<string> EditAsync(Category category)
		{
			await _categoryRepository.UpdateAsync(category);
			return "Success";
		}

		public async Task<string> DeleteAsync(Category category)
		{
			await _categoryRepository.DeleteAsync(category);
			return "Success";
		}
		#endregion

	}
}
