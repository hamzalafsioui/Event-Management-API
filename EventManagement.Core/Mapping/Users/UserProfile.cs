using AutoMapper;

namespace EventManagement.Core.Mapping.Users
{
	public partial class UserProfile : Profile
	{
		public UserProfile()
		{
			GetUserListMapping();
			GetUserByIdMapping();
			AddUserCommandMapping();
		}
	}
}
