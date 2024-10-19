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

		public virtual async Task<T> AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();
			return entity;
		}


		public virtual async Task AddRangeAsync(ICollection<T> entities)
		{
			await _dbContext.AddRangeAsync(entities);
			await _dbContext.SaveChangesAsync();
		}

		public virtual async Task DeleteAsync(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			await _dbContext.SaveChangesAsync();

		}

		public virtual async Task DeleteRangeAsync(ICollection<T> entities)
		{
			foreach (var entity in entities)
			{
				_dbContext.Entry(entity).State = EntityState.Deleted;
			}
			await _dbContext.SaveChangesAsync();
		}

		public virtual async Task UpdateAsync(T entity)
		{
			_dbContext.Set<T>().Update(entity);
			await _dbContext.SaveChangesAsync();
		}

		public virtual async Task UpdateRangeAsync(ICollection<T> entities)
		{
			_dbContext.Set<T>().UpdateRange(entities);
			await _dbContext.SaveChangesAsync();
		}
		public virtual async Task SaveChangesAsync()
		{
			await _dbContext.SaveChangesAsync();
		}
		public virtual IDbContextTransaction BeginTransaction()
		{
			return _dbContext.Database.BeginTransaction();
		}

		public virtual void Commit()
		{
			_dbContext.Database.CommitTransaction();
		}

		public virtual void RollBack()
		{
			_dbContext.Database.RollbackTransaction();
		}




		#endregion

	}
}
