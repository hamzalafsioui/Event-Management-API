using EventManagement.Core.Bases;
using EventManagement.Data.DTOs.Roles;
using MediatR;

namespace EventManagement.Core.Features.Authorization.Commands.Models
{
	public class UpdateUserRoleCommand() : ManageUserRolesRequest, IRequest<Response<string>>
	{

	}


}
