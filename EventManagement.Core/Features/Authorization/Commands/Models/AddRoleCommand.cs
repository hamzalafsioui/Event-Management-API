using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Authorization.Commands.Models
{
	public record AddRoleCommand(string RoleName) : IRequest<Response<string>>;

}
