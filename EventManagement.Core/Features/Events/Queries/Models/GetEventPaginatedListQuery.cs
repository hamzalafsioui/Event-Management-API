using EventManagement.Core.Features.Events.Queries.Responses;
using EventManagement.Core.Wrappers;
using EventManagement.Data.Helper.Enums;
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
