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
		private readonly ICacheService _cacheService;
		private const string CategoriesCacheKey = "CategoriesList";

		#endregion

		#region Constructors
		public CategoryService(ICategoryRepository categoryRepository, ICacheService cacheService)
		{
			_categoryRepository = categoryRepository;
			_cacheService = cacheService;
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
			var cachedCategories = await _cacheService.GetAsync<List<Category>>(CategoriesCacheKey);
			if (cachedCategories != null)
			{
				Console.WriteLine("-----> Data retrieved from REDIS CACHE");
				return cachedCategories;
			}

			Console.WriteLine("-----> Data retrieved from DATABASE");
			var result = await _categoryRepository.GetTableNoTracking().ToListAsync();
			await _cacheService.SetAsync(CategoriesCacheKey, result, TimeSpan.FromHours(2));
			return result;
		}

		public async Task<Category?> GetCategoryByIdAsync(int categoryId) => await _categoryRepository.GetTableNoTracking().Where(x => x.CategoryId.Equals(categoryId)).FirstOrDefaultAsync();
		public async Task<bool> IsCategoryNameExistAsync(string categoryName)
		{
			return await _categoryRepository.GetTableNoTracking()
											.AnyAsync(c => c.Name == categoryName);

		}
		public async Task<bool> IsCategoryNameExistExcludeSelfAsync(string categoryName, int categoryId)
		{
			return await _categoryRepository.GetTableNoTracking()
											.AnyAsync(c => c.Name == categoryName && c.CategoryId != categoryId);


		}
		public async Task<Category> AddAsync(Category category)
		{
			var result = await _categoryRepository.AddAsync(category);
			await _cacheService.RemoveAsync(CategoriesCacheKey);
			// Console.WriteLine("-----> REDIS CACHE INVALIDATED (Category Added)");
			return result;
		}
		public async Task<Category> EditAsync(Category category)
		{
			var result = await _categoryRepository.UpdateAsync(category);
			await _cacheService.RemoveAsync(CategoriesCacheKey);
			// Console.WriteLine("-----> REDIS CACHE INVALIDATED (Category Updated)");
			return result;
		}
		public async Task<bool> DeleteAsync(Category category)
		{
			var result = await _categoryRepository.DeleteAsync(category);
			await _cacheService.RemoveAsync(CategoriesCacheKey);
			// Console.WriteLine("-----> REDIS CACHE INVALIDATED (Category Deleted)");
			return result;
		}

		#endregion

	}
}
