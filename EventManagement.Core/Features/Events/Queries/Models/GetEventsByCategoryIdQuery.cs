using EventManagement.Core.Bases;
using EventManagement.Core.Features.Events.Queries.Response;
using MediatR;

namespace EventManagement.Core.Features.Events.Queries.Models
{
	public record GetEventsListByCategoryIdQuery(int categoryId) : IRequest<Response<List<GetEventsListByCategoryIdResponse>>>;

}
