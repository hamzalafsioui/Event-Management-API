using EventManagement.Core.Bases;
using EventManagement.Core.Features.Users.Queries.Results;
using EventManagement.Data.Entities;
using MediatR;

namespace EventManagement.Core.Features.Users.Queries.Models
{
	public class GetUserListQuery : IRequest<Response<List<GetUserListResponse>>>
	{
	}
}
