using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Comments.Commands.Models
{
	public class AddCommentCommand : IRequest<Response<string>>
	{
		public int eventId { get; set; }
		public int userId { get; set; }
		public string content { get; set; }
	}

}
