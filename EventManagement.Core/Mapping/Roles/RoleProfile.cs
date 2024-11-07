using AutoMapper;

namespace EventManagement.Core.Mapping.Roles
{
	public partial class RoleProfile : Profile
	{
        public RoleProfile()
        {
			GetRolesListMapping();

		}
    }
}
