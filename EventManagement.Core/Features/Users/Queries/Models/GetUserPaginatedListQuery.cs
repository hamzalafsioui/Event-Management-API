using EventManagement.Core.Features.Users.Queries.Results;
using EventManagement.Core.Wrappers;
using EventManagement.Data.Helper.Enums;
using MediatR;

namespace EventManagement.Core.Features.Users.Queries.Models
{
    public record GetUserPaginatedListQuery(
		int PageNumber,
		int PageSize,
		UserOrderingEnum OrderBy,
		string? Search
	) : IRequest<PaginatedResult<GetUserPaginatedListResponse>>;
}
