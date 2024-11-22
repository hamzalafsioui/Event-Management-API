using EventManagement.Core.Bases;
using EventManagement.Core.Features.Users.Queries.Responses;
using MediatR;

namespace EventManagement.Core.Features.Users.Queries.Models
{
	public record GetUserEventEngagementDetailsQuery(int UserId):IRequest<Response<GetUserEventEngagementDetailsResponse>>;
	
}
