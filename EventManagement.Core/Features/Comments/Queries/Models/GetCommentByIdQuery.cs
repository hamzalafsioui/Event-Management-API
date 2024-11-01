using EventManagement.Core.Bases;
using EventManagement.Core.Features.Comments.Queries.Responses;
using MediatR;

namespace EventManagement.Core.Features.Comments.Queries.Models
{
	public record GetCommentByIdQuery(int commentId) : IRequest<Response<GetCommentByIdResponse>>;

}
