using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace EventManagement.Infrustructure.InfrustructureBase
{
	/// <summary>
	/// A generic repository interface for performing CRUD operations and managing database transactions asynchronously.
	/// </summary>
	/// <typeparam name="T">The entity type for the repository, which must be a class.</typeparam>
	public interface IGenericRepositoryAsync<T> where T : class
	{
		/// <summary>
		/// Deletes a collection of <paramref name="entities"/> from the database asynchronously.
		/// </summary>
		/// <param name="entities">The collection of entities to delete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if the operation succeeded; otherwise, <see langword="false"/>.</returns>
		Task<bool> DeleteRangeAsync(ICollection<T> entities);

		/// <summary>
		/// Retrieves an entity by its <paramref name="id"/> asynchronously.
		/// </summary>
		/// <param name="id">The unique identifier of the entity.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="T"/> if found; otherwise, <see langword="null"/>.</returns>
		Task<T?> GetByIdAsync(int id);

		/// <summary>
		/// Begins a database transaction asynchronously.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IDbContextTransaction"/> representing the transaction.</returns>
		Task<IDbContextTransaction> BeginTransactionAsync();

		/// <summary>
		/// Commits the current transaction asynchronously.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation.</returns>
		Task CommitAsync();

		/// <summary>
		/// Rolls back the current transaction asynchronously.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation.</returns>
		Task RollBackAsync();

		/// <summary>
		/// Retrieves a queryable collection of entities with no tracking.
		/// </summary>
		/// <returns>A queryable collection of entities with no tracking, represented by <see cref="IQueryable{T}"/>.</returns>
		IQueryable<T> GetTableNoTracking();

		/// <summary>
		/// Retrieves a queryable collection of entities with tracking enabled.
		/// </summary>
		/// <returns>A queryable collection of entities with tracking enabled, represented by <see cref="IQueryable{T}"/>.</returns>
		IQueryable<T> GetTableAsTracking();

		/// <summary>
		/// Retrieves all entities from the database asynchronously.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation. The task result contains a collection of all <see cref="T"/> entities.</returns>
		Task<IEnumerable<T>> GetAllAsync();

		/// <summary>
		/// Adds a new <paramref name="entity"/> to the database asynchronously.
		/// </summary>
		/// <param name="entity">The <paramref name="entity"/> to add.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the added <see cref="T"/> entity.</returns>
		Task<T> AddAsync(T entity);

		/// <summary>
		/// Adds a collection of new <paramref name="entities"/> to the database asynchronously.
		/// </summary>
		/// <param name="entities">The collection of <paramref name="entities"/> to add.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the added <see cref="IEnumerable{T}"/> entities.</returns>
		Task<IEnumerable<T>> AddRangeAsync(ICollection<T> entities);

		/// <summary>
		/// Updates an existing <paramref name="entity"/> in the database asynchronously.
		/// </summary>
		/// <param name="entity">The <paramref name="entity"/> to update.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the updated <see cref="T"/> entity.</returns>
		Task<T> UpdateAsync(T entity);

		/// <summary>
		/// Updates a collection of existing <paramref name="entities"/> in the database asynchronously.
		/// </summary>
		/// <param name="entities">The collection of <paramref name="entities"/> to update.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if the operation succeeded; otherwise, <see langword="false"/>.</returns>
		Task<bool> UpdateRangeAsync(ICollection<T> entities);

		/// <summary>
		/// Deletes a <paramref name="entity"/> from the database asynchronously.
		/// </summary>
		/// <param name="entity">The <paramref name="entity"/> to delete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if the operation succeeded; otherwise, <see langword="false"/>.</returns>
		Task<bool> DeleteAsync(T entity);

		/// <summary>
		/// Finds entities in the database that match the specified <paramref name="expression"/> asynchronously.
		/// </summary>
		/// <param name="expression">The <paramref name="expression"/> to filter the entities.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="T"/> entities that match the condition.</returns>
		Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);

	}
}
