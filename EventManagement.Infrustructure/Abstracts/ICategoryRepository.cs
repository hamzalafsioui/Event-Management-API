using EventManagement.Data.Entities;
using EventManagement.Infrustructure.InfrustructureBase;

namespace EventManagement.Infrustructure.Repositories
{
	public interface ICategoryRepository : IGenericRepositoryAsync<Category>
	{
		public Task<List<Category>> GetCategoriesListAsync();

	}

}