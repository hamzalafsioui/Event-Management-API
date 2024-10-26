using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Helper.Authentication;
using EventManagement.Infrustructure.Abstracts;
using EventManagement.Infrustructure.Context;
using EventManagement.Infrustructure.InfrustructureBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrustructure.Repositories
{
	public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
	{
		#region Fields
		private readonly DbSet<UserRefreshToken> _userRefreshTokens;
		#endregion
		#region Constructors
		public RefreshTokenRepository(AppDbContext dbContext) : base(dbContext)
		{
			_userRefreshTokens = dbContext.Set<UserRefreshToken>();
		}
		#endregion
		#region Handle Functions

		#endregion

	}
}
