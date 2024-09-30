using EventManagement.Core.Features.Users.Queries.Results;
using EventManagement.Data.Entities;
using MediatR;

namespace EventManagement.Core.Features.Users.Queries.Models
{
	public class GetUserListQuery : IRequest<List<GetUserListResponse>>
	{
	}
}
