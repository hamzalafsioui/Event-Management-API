using EventManagement.Infrustructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

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

		public virtual async Task<T> GetByIdAsync(int id)
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

		public virtual async Task<bool> AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			return await _dbContext.SaveChangesAsync() > 0;

		}


		public virtual async Task<bool> AddRangeAsync(ICollection<T> entities)
		{
			await _dbContext.AddRangeAsync(entities);
			return await _dbContext.SaveChangesAsync() > 0;
		}

		public virtual async Task<bool> DeleteAsync(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			return await _dbContext.SaveChangesAsync() > 0;

		}

		public virtual async Task<bool> DeleteRangeAsync(ICollection<T> entities)
		{
			foreach (var entity in entities)
			{
				_dbContext.Entry(entity).State = EntityState.Deleted;
			}
			return await _dbContext.SaveChangesAsync() > 0;
		}

		public virtual async Task<bool> UpdateAsync(T entity)
		{
			_dbContext.Set<T>().Update(entity);
			return await _dbContext.SaveChangesAsync() > 0;
		}

		public virtual async Task<bool> UpdateRangeAsync(ICollection<T> entities)
		{
			_dbContext.Set<T>().UpdateRange(entities);
			return await _dbContext.SaveChangesAsync() > 0;
		}
		public virtual async Task SaveChangesAsync()
		{
			await _dbContext.SaveChangesAsync();
		}
		public virtual async Task<IDbContextTransaction> BeginTransactionAsync()
		{
			return await _dbContext.Database.BeginTransactionAsync();
		}

		public virtual async void CommitAsync()
		{
			await _dbContext.Database.CommitTransactionAsync();
		}

		public virtual async void RollBackAsync()
		{
			await _dbContext.Database.RollbackTransactionAsync();
		}




		#endregion

	}
}
