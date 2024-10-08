using EventManagement.Core.Bases;
using EventManagement.Data.Helper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EventManagement.Core.Features.Users.Commands.Models
{
	public class EditUserCommand : IRequest<Response<string>>
	{
		public int UserId { get; set; }
		public string Username { get; set; }
		public string PasswordHash { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public IFormFile? Image { get; set; }
		public UserRoleEnum Role { get; set; }

	}
}
