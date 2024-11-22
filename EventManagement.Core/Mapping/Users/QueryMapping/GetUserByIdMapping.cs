using EventManagement.Core.Features.Users.Queries.Responses;
using EventManagement.Data.Entities.Identity;

namespace EventManagement.Core.Mapping.Users
{
	public partial class UserProfile
	{
		public void GetUserByIdMapping()
		{
			CreateMap<User, GetSingleUserResponse>()
				.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
				.ForMember(dest => dest.Age,
				opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)));

		}
	}
}
