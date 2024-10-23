using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Data.Entities.Identity;

namespace EventManagement.Core.Mapping.Users
{
	public partial class UserProfile
	{
		public void EditUserCommandMapping()
		{
			CreateMap<EditUserCommand, User>();

		}
	}
}
