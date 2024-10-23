using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Users.Commands.Models
{
	public record DeleteUserCommand(int id) : IRequest<Response<string>>;
}
