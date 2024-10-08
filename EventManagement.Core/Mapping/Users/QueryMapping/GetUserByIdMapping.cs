using EventManagement.Core.Features.Users.Queries.Results;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Users
{
	public partial class UserProfile
	{
		public void GetUserByIdMapping()
		{
			CreateMap<User, GetSingleUserResponse>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
		}
	}
}
