using EventManagement.Core.Bases;
using EventManagement.Core.Features.Authorization.Queries.Responses;
using MediatR;

namespace EventManagement.Core.Features.Authorization.Queries.Models
{
	public record GetRoleByIdQuery(int Id) : IRequest<Response<GetRoleByIdResponse>>;

}
