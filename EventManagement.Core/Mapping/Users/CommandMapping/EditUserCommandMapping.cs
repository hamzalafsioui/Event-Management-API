using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Data.Entities.Identity;

namespace EventManagement.Core.Mapping.Users
{
    public partial class UserProfile
	{
		public void EditUserCommandMapping()
		{
			CreateMap<EditUserCommand, User>()
				.ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
				.ForMember(dest => dest.Id, opt => opt.Ignore());
			//.ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));


		}
	}
}
