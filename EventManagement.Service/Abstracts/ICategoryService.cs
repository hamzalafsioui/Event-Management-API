using EventManagement.Data.Entities;

namespace EventManagement.Service.Abstracts
{
	/// <summary>
	/// Interface defining operations related to event categories.
	/// </summary>
	public interface ICategoryService
	{
		/// <summary>
		/// Checks if a category exists in the database by its <paramref name="categoryId"/> asynchronously.
		/// </summary>
		/// <param name="categoryId">The unique identifier of the category.</param>
		/// <returns><c><see langword="true"/></c> if the category exists; otherwise, <c><see langword="true"/></c>.</returns>
		public Task<bool> IsCategoryIdExist(int categoryId);

		/// <summary>
		/// Retrieves a list of all categories asynchronously.
		/// </summary>
		/// <returns>A list of <see cref="Category"/> entities.</returns>
		public Task<List<Category>> GetCategoriesListAsync();

		/// <summary>
		/// Retrieves a category by its <paramref name="categoryId"/> asynchronously.
		/// </summary>
		/// <param name="categoryId">The unique identifier of the category.</param>
		/// <returns>The <see cref="Category"/> entity if found; otherwise, <c><see langword="null"/></c>.</returns>
		public Task<Category?> GetCategoryByIdAsync(int categoryId);

		/// <summary>
		/// Checks if a <paramref name="categoryName"/> exists in the system asynchronously.
		/// </summary>
		/// <param name="categoryName">The name of the category to check.</param>
		/// <returns><c><see langword="true"/></c> if the category name exists; otherwise, <c><see langword="false"/></c>.</returns>
		public Task<bool> IsCategoryNameExistAsync(string categoryName);

		/// <summary>
		/// Checks if a <paramref name="categoryName"/> exists in the system, excluding the category with the specified <paramref name="categoryId"/> asynchronously.
		/// </summary>
		/// <param name="categoryName">The name of the category to check.</param>
		/// <param name="categoryId">The unique identifier of the category to exclude.</param>
		/// <returns><c><see langword="true"/></c> if the <paramref name="categoryName"/> exists; otherwise, <c><see langword="false"/></c>.</returns>
		public Task<bool> IsCategoryNameExistExcludeSelfAsync(string categoryName, int categoryId);

		/// <summary>
		/// Adds a new <paramref name="category"/> to the database asynchronously.
		/// </summary>
		/// <param name="category">The <see cref="Category"/> entity to add.</param>
		/// <returns>The added <see cref="Category"/> entity.</returns>
		public Task<Category> AddAsync(Category category);

		/// <summary>
		/// Updates an existing <paramref name="category"/> in the database asynchronously.
		/// </summary>
		/// <param name="category">The <see cref="Category"/> entity to update.</param>
		/// <returns>The updated <see cref="Category"/> entity.</returns>
		public Task<Category> EditAsync(Category category);

		/// <summary>
		/// Deletes a <paramref name="category"/> from the database asynchronously.
		/// </summary>
		/// <param name="category">The <see cref="Category"/> entity to delete.</param>
		/// <returns><c><see langword="true"/></c> if the delete operation succeeded; otherwise, <c><see langword="false"/></c>.</returns>
		public Task<bool> DeleteAsync(Category category);
	}
}
