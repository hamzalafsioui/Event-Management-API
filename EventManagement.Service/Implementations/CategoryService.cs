﻿using EventManagement.Data.Entities;
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
			var category = await _categoryRepository.GetByIdAsync(categoryId);
			return category;
		}
		#endregion

	}
}
