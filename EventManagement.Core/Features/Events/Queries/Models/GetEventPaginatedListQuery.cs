using EventManagement.Core.Features.Events.Queries.Response;
using EventManagement.Core.Wrappers;
using EventManagement.Data.Helper;
using MediatR;

namespace EventManagement.Core.Features.Events.Queries.Models
{
	public class GetEventPaginatedListQuery:IRequest<PaginatedResult<GetEventPaginatedListResponse>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public EventOrderingEnum OrderBy { get; set; }
		public string? Search { get; set; }
	}
}
