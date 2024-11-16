using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace EventManagement.Infrustructure.InfrustructureBase
{
	public interface IGenericRepositoryAsync<T> where T : class
	{
		Task<bool> DeleteRangeAsync(ICollection<T> entities);
		Task<T?> GetByIdAsync(int id);
		Task<IDbContextTransaction> BeginTransactionAsync();
		Task CommitAsync();
		Task RollBackAsync();
		IQueryable<T> GetTableNoTracking();
		IQueryable<T> GetTableAsTracking();
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> AddAsync(T entity);
		Task<IEnumerable<T>> AddRangeAsync(ICollection<T> entities);
		Task<T> UpdateAsync(T entity);
		Task<bool> UpdateRangeAsync(ICollection<T> entities);
		Task<bool> DeleteAsync(T entity);
		Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);

	}
}
