using EventManagement.Core.Features.Authorization.Queries.Responses;
using EventManagement.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Core.Mapping.Roles
{
	public partial class RoleProfile
	{
		private void GetRolesListMapping()
		{
			CreateMap<Role, GetRolesListResponse>();
		}
	}
}
