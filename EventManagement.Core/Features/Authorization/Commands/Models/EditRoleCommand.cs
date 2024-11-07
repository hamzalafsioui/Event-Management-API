using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Authorization.Commands.Models
{
	public record EditRoleCommand(int Id,string Name):IRequest<Response<string>>;
	
}
