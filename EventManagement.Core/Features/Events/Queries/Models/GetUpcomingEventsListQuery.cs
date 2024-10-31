using EventManagement.Core.Bases;
using EventManagement.Core.Features.Events.Queries.Response;
using EventManagement.Data.Helper.Enums;
using MediatR;

namespace EventManagement.Core.Features.Events.Queries.Models
{
	public record GetUpcomingOrPastEventsListQuery(DateTimeComparison Comparison) : IRequest<Response<List<GetUpcomingOrPastEventsListResponse>>>;

}
