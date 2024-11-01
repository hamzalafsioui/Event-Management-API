using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Comments.Commands.Models
{
	public record EditCommentCommand(int commentId, string content) : IRequest<Response<string>>;

}
