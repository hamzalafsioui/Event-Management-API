using EventManagement.Core.Bases;
using EventManagement.Core.Features.Attendees.Queries.Responses;
using MediatR;

namespace EventManagement.Core.Features.Attendees.Queries.Models
{
	public record GetRegisteredEventsForUserQuery(int userId) : IRequest<Response<List<GetRegisteredEventsListForUserResponse>>>;

}
