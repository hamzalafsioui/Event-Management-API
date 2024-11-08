using EventManagement.Core.Bases;
using EventManagement.Data.Requests;
using MediatR;

namespace EventManagement.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserRoleCommand() : ManageUserRolesRequest, IRequest<Response<string>>
	{

	}


}
