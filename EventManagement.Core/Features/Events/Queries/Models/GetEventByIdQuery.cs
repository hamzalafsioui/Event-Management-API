using EventManagement.Core.Bases;
using EventManagement.Core.Features.Events.Queries.Response;
using MediatR;

namespace EventManagement.Core.Features.Events.Queries.Models
{
	public class GetEventByIdQuery:IRequest<Response<GetEventByIdResponse>>
	{
		public int Id { get; set; }
		public int AttendeePageNumber { get; set; }
		public int AttendeePageSize { get; set; }
		public int CommentPageNumber { get; set; }
		public int CommentPageSize { get; set; }

      
    }
}
