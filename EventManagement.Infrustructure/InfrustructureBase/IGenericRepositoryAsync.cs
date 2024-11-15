using Microsoft.EntityFrameworkCore.Storage;

namespace EventManagement.Infrustructure.InfrustructureBase
{
	public interface IGenericRepositoryAsync<T> where T : class
	{
		Task<bool> DeleteRangeAsync(ICollection<T> entities);
		Task<T> GetByIdAsync(int id);
		Task SaveChangesAsync();
		Task<IDbContextTransaction> BeginTransactionAsync();
		Task CommitAsync();
		Task RollBackAsync();
		IQueryable<T> GetTableNoTracking();
		IQueryable<T> GetTableAsTracking();
		Task<bool> AddAsync(T entity);
		Task<bool> AddRangeAsync(ICollection<T> entities);
		Task<bool> UpdateAsync(T entity);
		Task<bool> UpdateRangeAsync(ICollection<T> entities);
		Task<bool> DeleteAsync(T entity);
	}
}
