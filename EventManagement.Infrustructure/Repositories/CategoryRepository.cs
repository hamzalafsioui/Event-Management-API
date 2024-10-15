using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Context;
using EventManagement.Infrustructure.InfrustructureBase;
using EventManagement.Infrustructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrustructure.Abstracts
{
	public class CategoryRepository : GenericRepositoryAsync<Category>, ICategoryRepository
	{
		#region Fields
		private readonly DbSet<Category> _categories;
		#endregion

		#region Constructors
		public CategoryRepository(AppDbContext dbContext) : base(dbContext)
		{
			_categories = dbContext.Set<Category>();
		}


		#endregion

		#region Handl Functions
		public async Task<List<Category>> GetCategoriesListAsync()
		{
			return await _categories.ToListAsync();
		}


		#endregion

	}
}


