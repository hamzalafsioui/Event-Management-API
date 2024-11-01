using EventManagement.Core.Bases;
using EventManagement.Core.Features.Users.Queries.Results;
using MediatR;

namespace EventManagement.Core.Features.Users.Queries.Models
{
	public record GetUserCommentsQuery(int userId) : IRequest<Response<List<GetUserCommentsResponse>>>;

}
