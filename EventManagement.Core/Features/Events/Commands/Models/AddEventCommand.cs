using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Events.Commands.Models
{
	public class AddEventCommand : IRequest<Response<string>>
	{
		public required string Title { get; set; }
		public string? Description { get; set; } = string.Empty;
		public required string Location { get; set; }
		public required DateTime StartTime { get; set; } // validate always StartTime before EndTime
		public required DateTime EndTime { get; set; }
		public required int CategoryId { get; set; }
		public required int CreatorUserId { get; set; } // in the future will be removed and fill it automatic by request (userId)
		public required int Capacity { get; set; }
	}
}
