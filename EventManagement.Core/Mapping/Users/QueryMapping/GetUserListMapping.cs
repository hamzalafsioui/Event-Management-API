using EventManagement.Core.Features.Users.Queries.Results;
using EventManagement.Data.Entities;
namespace EventManagement.Core.Mapping.Users
{
	public partial class UserProfile
	{
		
		public void GetUserListMapping()
		{
			CreateMap<User, GetUserListResponse>();
			  // .ForMember(destination => destination.DepartmentName, opt => opt.MapFrom(source => source.Department.DName));

		}
	}
}
