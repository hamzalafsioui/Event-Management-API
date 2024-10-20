using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Events.Commands.Models
{
	public class EditEventCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string? Description { get; set; } = string.Empty;
		public string Location { get; set; }
		public DateTime StartTime { get; set; } // validate always StartTime before EndTime
		public DateTime EndTime { get; set; }
		public int CategoryId { get; set; }
		public int CreatorUserId { get; set; } // in the future will be removed and fill it automatic by request (userId)
		public int Capacity { get; set; }
	}
}
