using EventManagement.Data.Entities;
using MediatR;

namespace EventManagement.Core.Features.Users.Queries.Models
{
	public class GetUserListQuery : IRequest<List<User>>
	{
	}
}
