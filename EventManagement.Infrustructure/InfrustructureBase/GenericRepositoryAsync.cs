using EventManagement.Infrustructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace EventManagement.Infrustructure.InfrustructureBase
{
	public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
	{
		#region Fields
		private readonly AppDbContext _dbContext;

		#endregion

		#region Constructors
		public GenericRepositoryAsync(AppDbContext dbContext)
		{
			this._dbContext = dbContext;
		}
		#endregion

		#region Methods

		#endregion

		#region Actions

		public virtual async Task<T?> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}
		public virtual IQueryable<T> GetTableAsTracking()
		{
			return _dbContext.Set<T>().AsTracking().AsQueryable();
		}

		public virtual IQueryable<T> GetTableNoTracking()
		{
			return _dbContext.Set<T>().AsNoTracking().AsQueryable();
		}

		public virtual async Task<T> AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();
			return entity;
		}


		public virtual async Task<IEnumerable<T>> AddRangeAsync(ICollection<T> entities)
		{
			await _dbContext.Set<T>().AddRangeAsync(entities);
			await _dbContext.SaveChangesAsync();
			return entities;
		}

		public virtual async Task<bool> DeleteAsync(T entity)
		{
			if (entity == null) return false;
			_dbContext.Set<T>().Remove(entity);
			return await _dbContext.SaveChangesAsync() > 0;

		}

		public virtual async Task<bool> DeleteRangeAsync(ICollection<T> entities)
		{
			_dbContext.RemoveRange(entities);
			return await _dbContext.SaveChangesAsync() > 0;
		}

		public virtual async Task<T> UpdateAsync(T entity)
		{
			_dbContext.Set<T>().Update(entity);
			await _dbContext.SaveChangesAsync();
			return entity;
		}

		public virtual async Task<bool> UpdateRangeAsync(ICollection<T> entities)
		{
			_dbContext.Set<T>().UpdateRange(entities);
			return await _dbContext.SaveChangesAsync() > 0;
		}
		
		public virtual async Task<IDbContextTransaction> BeginTransactionAsync()
		{
			return await _dbContext.Database.BeginTransactionAsync();
		}

		public virtual async Task CommitAsync()
		{
			await _dbContext.Database.CommitTransactionAsync();
		}

		public virtual async Task RollBackAsync()
		{
			await _dbContext.Database.RollbackTransactionAsync();
		}

		public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
		{
			return await _dbContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
		}

		#endregion

	}
}
