using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Context;
using EventManagement.Infrustructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrustructure.Abstracts
{
	public class UserRepository : IUserRepository
	{
        #region Fields
        private AppDbContext _dbContext;
        #endregion

        #region Constructors
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Handl Functions
        public async Task<List<User>> GetUsersListAsync()
		{
			return await _dbContext.Users.ToListAsync();
		}
		#endregion

	}
}
