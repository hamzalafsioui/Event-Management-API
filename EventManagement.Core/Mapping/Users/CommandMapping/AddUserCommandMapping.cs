using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Users
{
	public partial class UserProfile
	{
		public void AddUserCommandMapping()
		{
			CreateMap<AddUserCommand, User>();
		}
	}
}
