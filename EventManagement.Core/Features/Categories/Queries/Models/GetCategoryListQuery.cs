using EventManagement.Core.Bases;
using EventManagement.Core.Features.Categories.Queries.Responses;
using MediatR;

namespace EventManagement.Core.Features.Categories.Queries.Models
{
	public record GetCategoryListQuery() : IRequest<Response<List<GetCategoryListResponse>>>;

}
