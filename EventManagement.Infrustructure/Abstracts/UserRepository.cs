using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Context;
using EventManagement.Infrustructure.InfrustructureBase;
using EventManagement.Infrustructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrustructure.Abstracts
{
	public class UserRepository : GenericRepositoryAsync<User>,IUserRepository
	{
        #region Fields
        private readonly DbSet<User> _users;
        #endregion

        #region Constructors
        public UserRepository(AppDbContext dbContext):base(dbContext) 
        {
            _users = dbContext.Set<User>();
        }
        #endregion

        #region Handl Functions
        public async Task<List<User>> GetUsersListAsync()
		{
			return await _users.ToListAsync();
		}
		#endregion

	}
}
