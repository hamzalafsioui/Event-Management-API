using EventManagement.Core.Bases;
using EventManagement.Core.Features.Events.Queries.Response;
using MediatR;

namespace EventManagement.Core.Features.Events.Queries.Models
{
	public record GetCommentsListByEventIdQuery(int eventId) : IRequest<Response<List<GetCommentsListByEventIdResponse>>>;


}
