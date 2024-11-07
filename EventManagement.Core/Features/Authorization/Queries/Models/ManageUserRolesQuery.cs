using EventManagement.Core.Bases;
using EventManagement.Core.Features.Authorization.Queries.Responses;
using EventManagement.Data.DTOs.Roles;
using MediatR;

namespace EventManagement.Core.Features.Authorization.Queries.Models
{
	public record class ManageUserRolesQuery(int UserId) : IRequest<Response<ManageUserRolesResponse>>;


}
