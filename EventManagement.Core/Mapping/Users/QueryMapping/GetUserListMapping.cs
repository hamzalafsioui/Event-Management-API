using EventManagement.Core.Features.Users.Queries.Responses;
using EventManagement.Data.Entities.Identity;
namespace EventManagement.Core.Mapping.Users
{
	public partial class UserProfile
	{

		public void GetUserListMapping()
		{
			CreateMap<User, GetUserListResponse>()
				.ForMember(destination => destination.Role, option => option.MapFrom(source => source.Role.ToString()))
				.ForMember(dest => dest.Age,
				opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)));

			CreateMap<User, GetUserPaginatedListResponse>()
				.ForMember(destination => destination.Role, option => option.MapFrom(source => source.Role.ToString()))
				.ForMember(dest => dest.Age,
				opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)));

		}


	}
}
