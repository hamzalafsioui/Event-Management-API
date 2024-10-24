﻿using EventManagement.Core.Bases;
using EventManagement.Data.Helper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EventManagement.Core.Features.Users.Commands.Models
{
	public record EditUserCommand : IRequest<Response<string>>
	{
		public required int Id { get; init; }
		public required string Username { get; init; }
		public required string FirstName { get; init; }
		public required string LastName { get; init; }
		public required DateTime DateOfBirth { get; init; }
		public required string Email { get; init; }
		public IFormFile? Image { get; init; }
		public required UserRoleEnum Role { get; init; }

	}
}
