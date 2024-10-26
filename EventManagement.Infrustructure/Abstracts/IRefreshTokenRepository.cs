using EventManagement.Data.Entities.Identity;
using EventManagement.Infrustructure.InfrustructureBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrustructure.Abstracts
{
	public interface IRefreshTokenRepository:IGenericRepositoryAsync<UserRefreshToken>
	{
	}
}
