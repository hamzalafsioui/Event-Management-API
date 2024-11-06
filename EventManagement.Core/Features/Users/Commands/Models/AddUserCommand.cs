using EventManagement.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EventManagement.Core.Features.Users.Commands.Models
{
	public class AddUserCommand : IRequest<Response<string>>
	{
		public required string UserName { get; init; }
		public required string Password { get; init; }
		public required string ConfirmPassword { get; init; }
		public required string FirstName { get; init; }
		public required string LastName { get; init; }
		public required DateTime DateOfBirth { get; init; }
		public required string Email { get; init; }
		public IFormFile? Image { get; init; }
	}
}
