using EventManagement.Core.Bases;
using EventManagement.Data.Helper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EventManagement.Core.Features.Users.Commands.Models
{
	public class AddUserCommand : IRequest<Response<string>>
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Email { get; set; }
		public IFormFile? Image { get; set; }
		public UserRoleEnum Role { get; set; } = UserRoleEnum.Attendee;
	}
}
