using EventManagement.Core.Features.Users.Queries.Results;
using EventManagement.Core.Wrappers;
using MediatR;

namespace EventManagement.Core.Features.Users.Queries.Models
{
	public class GetUserPaginatedListQuery :IRequest<PaginatedResult<GetUserPaginatedListResponse>>
	{
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string []? OrderBy { get; set; }
        public string? Search { get; set; }
    } 
}
