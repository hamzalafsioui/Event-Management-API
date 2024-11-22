using EventManagement.Core.Bases;
using EventManagement.Core.Features.Users.Queries.Responses;
using MediatR;

namespace EventManagement.Core.Features.Users.Queries.Models
{
	public record GetUserByIdQuery(int Id) : IRequest<Response<GetSingleUserResponse>>;
}
