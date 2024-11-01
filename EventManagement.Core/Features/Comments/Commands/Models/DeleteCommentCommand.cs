using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Comments.Commands.Models
{
	public record DeleteCommentCommand(int commentId) : IRequest<Response<string>>;

}
